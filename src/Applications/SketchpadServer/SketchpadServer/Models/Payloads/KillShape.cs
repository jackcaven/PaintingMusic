using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SketchpadServer.Models.Payloads
{
    public class KillShape
    {
        [JsonProperty("sender")]
        public required string Sender { get; set; }
        [JsonProperty("command")]
        public required string Command { get; set; }
        [JsonProperty("payload")]
        public required KillPayload Payload { get; set; }
    }

    public class KillPayload
    {
        [JsonProperty("hash")]
        public required string ShapeID { get; set; }
    }
}
