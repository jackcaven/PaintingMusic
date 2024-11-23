namespace Core.DataStructures.Art
{
    public record ObjectAttributes
    {
        public Guid Id { get; set; }
        public double ColorR { get; set; }
        public double ColorG { get; set; }
        public double ColorB { get; set; }
        public double Temperature { get; set; }
        public double Hue { get; set; }
        public double Tone { get; set; }
        public double Area { get; set; }
        public double Complexity { get; set; }
        public required (double X, double Y) CanvasLocation { get; set; }
        public required (double X, double Y) COG { get; set; }
    }
}
