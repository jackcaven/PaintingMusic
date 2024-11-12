namespace Core.DataStructures.Music
{
    public record Note
    {
        public required IEnumerable<int> Notes { get; set; }
        public int Velocity { get; set; }
        public double StartTime { get; set; }
        public double Duration { get; set; }
    }
}
