namespace Core.DataStructures.Art
{
    public record CanvasAttributes
    {
        public double AreaCovered { get; set; }
        public required (double X, double Y) COG { get; set; }
    }
}
