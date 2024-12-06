using Newtonsoft.Json;

namespace SketchpadServer.Models.Payloads
{
    public class NotePayload
    {
        [JsonProperty("notes")]
        public required IEnumerable<int> Notes { get; set; }

        [JsonProperty("velocity")]
        public int Velocity { get; set; }

        [JsonProperty("start_time")]
        public double StartTime { get; set; }

        [JsonProperty("duration")]
        public double Duration { get; set; }


    }
}
