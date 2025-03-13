namespace Core.DataStructures.Music
{
    public record MusicAttributes
    {
        public int Pitch { get; init; }
        public int Velocity { get; init; }
        public int NoteLength { get; init; }
        public int NumberOfNotes { get; init; }
        public int BPM { get; init; }
    }
}
