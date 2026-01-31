namespace CanvasCaptureVLM.Classes.MusicClient.Strudel.Models
{
    internal class LoginResponse
    {
        public bool Success { get; set; }
        public string Token { get; set; } = string.Empty;
        public int ExpiresIn { get; set; }
    }
}
