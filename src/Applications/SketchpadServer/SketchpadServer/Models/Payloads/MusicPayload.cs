using Core.DataStructures.Music;
using Newtonsoft.Json;

namespace SketchpadServer.Models.Payloads
{
    public class MusicPayload
    {
        [JsonProperty(PropertyName = "id")]
        public required string MessageID { get; set; }

        [JsonProperty(PropertyName = "sender")]
        public required string Sender { get; set; }

        [JsonProperty(PropertyName = "responseToMessageID")]
        public required string ResponseToMessageID { get; set; }

        [JsonProperty(PropertyName = "command")]
        public required string Command { get; set; }

        [JsonProperty(PropertyName = "bpm")]
        public required int BPM { get; set; }

        [JsonProperty(PropertyName = "instrument")]
        public required string Instrument {  get; set; }

        [JsonProperty(PropertyName = "notes")]
       public required List<NotePayload> Notes { get; set; }
    }
}
