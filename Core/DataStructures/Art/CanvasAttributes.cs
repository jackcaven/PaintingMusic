namespace Core.DataStructures.Art
{
    public record CanvasAttributes
    {
        public double AreaCovered { get; set; }
        public required Tuple<double, double> COG { get; set; }
    }
}
