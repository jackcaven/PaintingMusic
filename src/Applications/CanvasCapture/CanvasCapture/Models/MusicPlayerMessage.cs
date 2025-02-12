using Core.DataStructures.Music;

namespace CanvasCapture.Models
{
    public record MusicPlayerMessage(string command, MusicData? payload);

    public record MuiscPlayerInitMessage(string command, string payload);
}
