using Newtonsoft.Json;

namespace SketchpadServer.Models.Payloads
{
    public class UpdateShapes
    {   
        [JsonProperty("sender")]
        public required string Sender { get; set; }

        [JsonProperty("command")]
        public required string Command { get; set; }

        [JsonProperty("payload")]
        public required Payload Payload { get; set; }

        [JsonProperty("messageID")]
        public required string MessageID { get; set; }

        [JsonProperty("responseToMessageID")]
        public required string ResponseToMessageID { get; set; }       
    }

    public class Payload
    {
        [JsonProperty("mode")]
        public required string Mode { get; set; }

        [JsonProperty("shapes")]
        public required List<Shape> Shapes { get; set; }
    }

    public class Shape
    {
        [JsonProperty("centre")]
        public required List<double> Centre { get; set; }

        [JsonProperty("sides")]
        public required int Sides { get; set; }

        [JsonProperty("area")]
        public required double Area { get; set; }

        [JsonProperty("complexity")]
        public required double Complexity { get; set; }

        [JsonProperty("length")]
        public required double Length { get; set; }

        [JsonProperty("points")]
        public required List<List<double>> Points { get; set; }

        [JsonProperty("color")]
        public required string Color { get; set; }

        [JsonProperty("id")]
        public required string Id { get; set; }
    }
}
