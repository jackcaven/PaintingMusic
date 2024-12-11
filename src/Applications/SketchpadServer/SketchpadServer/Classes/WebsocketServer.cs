using ConsoleTools.Helpers;
using Core.DataStructures.Music;
using Core.Enums.AI;
using Microsoft.Extensions.Logging;
using MusicGeneration;
using Newtonsoft.Json;
using SketchpadServer.Enums;
using SketchpadServer.Helpers;
using SketchpadServer.Models.Payloads;
using System.Net;
using System.Net.WebSockets;
using System.Text;

namespace SketchpadServer.Classes
{
    public class WebsocketServer(ILogger<WebsocketServer> logger)
    {
        private const string applicationTitle = "Sketchpad";
        private const string serverName = "back-end";
        private const string defaultUrl = "http://localhost:5001/";

        private readonly ILogger<WebsocketServer> _logger = logger;

        public async Task Run()
        {
            Title.Show(applicationTitle);
            _logger.LogDebug("Starting websocket server...");

            var httpListener = new HttpListener();
            httpListener.Prefixes.Add("http://localhost:5001/");
            httpListener.Start();

            _logger.LogDebug("Websocket server is listening on {0}", defaultUrl);

            Console.WriteLine("For Sketchpad, navigate to: https://ray.scot/pm/public/");

            while (true)
            {
                try
                {
                    var context = await httpListener.GetContextAsync();

                    if (context.Request.IsWebSocketRequest)
                    {
                        var wsContext = await context.AcceptWebSocketAsync(null);
                        _logger.LogDebug("WebSocket connection established.");
                        await HandleWebSocketConnection(wsContext.WebSocket);
                    }
                    else
                    {
                        context.Response.StatusCode = 400; // Bad Request
                        context.Response.Close();
                        _logger.LogWarning("Rejected non-WebSocket request.");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error: {0}", ex.Message);
                }
            }

        }

        private async Task HandleWebSocketConnection(WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            CoreAdapter coreAdapter = new(CoreMusicGeneratorFactory.ConstructMusicGenerator(Model.Markov));

            try
            {
                while (webSocket.State == WebSocketState.Open)
                {
                    var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        _logger.LogDebug("Client disconnected.");
                        await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
                    }
                    else
                    {
                        var receivedMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);
                        _logger.LogDebug("Message Received: {0}", receivedMessage);

                        UpdateShapes? payload = JsonConvert.DeserializeObject<UpdateShapes>(receivedMessage);

                        if (payload == null || !PayloadVerifier.VerifyPayload(payload))
                        {
                            _logger.LogWarning("Invalid payload recieved");
                        }

                        if (!PayloadVerifier.GetCommand(payload!.Command, out Command command))
                        {
                            _logger.LogWarning("Invalid Command: {command}", payload!.Command);
                            continue;
                        }

                        if (command == Command.Update)
                        {
                            MusicData music = coreAdapter.GenerateMusic(payload!);

                            List<NotePayload> notePayload = [];

                            foreach (Note note in music.Notes)
                            {
                                notePayload.Add(new()
                                {
                                    Notes = note.Notes,
                                    StartTime = note.StartTime,
                                    Velocity = note.Velocity,
                                    Duration = note.Duration,
                                });
                            }

                            MusicPayload musicPayload = new()
                            {
                                Sender = serverName,
                                Command = "newPart",
                                MessageID = payload!.Payload.Shapes.First().Id,
                                ResponseToMessageID = payload.ResponseToMessageID,
                                Instrument = coreAdapter.GetInstrument(),
                                BPM = music.BPM,
                                Notes = notePayload
                            };

                            string jsonReturnPayload = JsonConvert.SerializeObject(musicPayload);
                            byte[] responseBuffer = Encoding.UTF8.GetBytes(jsonReturnPayload);
                            await webSocket.SendAsync(new ArraySegment<byte>(responseBuffer), WebSocketMessageType.Text, true, CancellationToken.None);
                        }
                        else if (command == Command.Kill)
                        {
                            KillShape? killPayload = JsonConvert.DeserializeObject<KillShape>(receivedMessage);

                            if (killPayload != null)
                            {
                                coreAdapter.DeleteShape(killPayload.Payload.ShapeID);
                            }
                        }
                        else if (command == Command.Reset)
                        {
                            coreAdapter.Clear();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error {e}", ex.Message);
            }
            finally
            {
                webSocket?.Dispose();
            }
        }
    }
}
