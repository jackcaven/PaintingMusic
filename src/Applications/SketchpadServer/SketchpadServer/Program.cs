using ConsoleTools.Helpers;
using Core.DataStructures.Music;
using Core.Enums.AI;
using MusicGeneration;
using Newtonsoft.Json;
using SketchpadServer.Classes;
using SketchpadServer.Enums;
using SketchpadServer.Helpers;
using SketchpadServer.Models.Payloads;
using System.Net;
using System.Net.WebSockets;
using System.Text;

class Program
{
    private const string applicationTitle = "Sketchpad";
    private const string serverName = "back-end";
    static async Task Main(string[] args)
    {
        Title.Show(applicationTitle);
        Console.WriteLine("Starting WebSocket server...");

        var httpListener = new HttpListener();
        httpListener.Prefixes.Add("http://localhost:5001/");
        httpListener.Start();

        Console.WriteLine("WebSocket server is listening on ws://localhost:5001");

        while (true)
        {
            try
            {
                var context = await httpListener.GetContextAsync();

                if (context.Request.IsWebSocketRequest)
                {
                    var wsContext = await context.AcceptWebSocketAsync(null);
                    Console.WriteLine("WebSocket connection established.");
                    await HandleWebSocketConnection(wsContext.WebSocket);
                }
                else
                {
                    context.Response.StatusCode = 400; // Bad Request
                    context.Response.Close();
                    Console.WriteLine("Rejected non-WebSocket request.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    private static async Task HandleWebSocketConnection(WebSocket webSocket)
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
                    Console.WriteLine("Client disconnected.");
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
                }
                else
                {
                    var receivedMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    Console.WriteLine($"Received: {receivedMessage}");

                    UpdateShapes? payload = JsonConvert.DeserializeObject<UpdateShapes>(receivedMessage);

                    if (payload == null || !PayloadVerifier.VerifyPayload(payload))
                    {
                        Console.WriteLine("Invalid payload recieved");
                    }

                    Command command = PayloadVerifier.Commands[payload?.Command!];

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
                            Instrument = "piano",
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
            Console.WriteLine($"Connection error: {ex.Message}");
        }
        finally
        {
            webSocket?.Dispose();
        }
    }
}
