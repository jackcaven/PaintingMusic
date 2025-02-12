using CanvasCapture.Models;
using Core.DataStructures.Music;
using Newtonsoft.Json;
using Serilog;
using System.Net.Sockets;

namespace CanvasCaptureUI.Classes
{
    internal class MusicPlayerClient
    {
        private const string ip = "127.0.0.1";
        private const int port = 12345;

        private readonly TcpClient client = new();
        private NetworkStream? networkStream;
        private StreamWriter? streamWriter;
        private StreamReader? streamReader;

        public async Task Start()
        {
            try
            {
                Log.Information("Attempting to connect to Painting Music Player");
                await client.ConnectAsync(ip, port);
                networkStream = client.GetStream();
                streamWriter = new(networkStream) { AutoFlush = true };
                streamReader = new StreamReader(networkStream);
                Log.Information("Connected to server");

                Log.Information("Sending start message");

                MusicPlayerMessage startMessage = new("start", null);

                await Send(startMessage);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to connect to server");
            }
        }

        public async Task SendPayload(MusicData musicData)
        {
            try
            {
                MusicPlayerMessage message = new("music payload", musicData);

                await Send(message);
            }
            catch (Exception ex)
            {
                Log.Error("Failed to send payload: {Payload}", musicData.ToString(), ex);
            }
        }

        public async Task Stop()
        {
            try
            {
                Log.Information("Stopping Painting Music Player");
                MusicPlayerMessage musicMessage = new("stop", null);
                await Send(musicMessage);

                streamReader?.Close();
                streamWriter?.Close();
                streamReader?.Close();
                client?.Close();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to send stop message");
            }
        }

        private async Task Send(MusicPlayerMessage musicPlayerMessage)
        {
            try
            {
                if (client == null || !client.Connected)
                {
                    Log.Error("Server not connected!");
                    return;
                }

                string message = JsonConvert.SerializeObject(musicPlayerMessage).ToLower().Replace("starttime", "start_time");

                if (musicPlayerMessage.payload == null)
                {
                    message = message.Replace("null", "\"\"");
                }

                await streamWriter!.WriteAsync(message);

                Log.Information("Music data sent successfully");

            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to send to server");
            }
        }
    }
}
