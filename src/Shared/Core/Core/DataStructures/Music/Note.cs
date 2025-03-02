namespace Core.DataStructures.Music
{
    public record Note
    {
        public required IEnumerable<int> Notes { get; set; }
        public int Velocity { get; set; }
        public double StartTime { get; set; }
        public double Duration { get; set; }

        public override string ToString()
        {
            var notesString = string.Join(", ", Notes);

            return $"Notes: [{notesString}], Velocity: {Velocity}, StartTime: {StartTime}, Duration: {Duration}";
        }
    }
}
