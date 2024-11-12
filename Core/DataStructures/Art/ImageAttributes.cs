namespace Core.DataStructures.Art
{
    public record ImageAttributes
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
        public required Tuple<double, double> CanvasLocation { get; set; }
        public required Tuple<double, double> COG { get; set; }
    }
}
