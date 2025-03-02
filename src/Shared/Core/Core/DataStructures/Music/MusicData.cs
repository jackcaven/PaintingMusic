namespace Core.DataStructures.Music
{
    public record MusicData
    {
        public required string Instrument { get; set; }
        public int BPM { get; set; }

        public required List<Note> Notes { get; set; }

        public override string ToString()
        {
            var notesString = string.Join(" | ", Notes.Select(note => note.ToString()));

            return $"Instrument: {Instrument}, BPM: {BPM}, Notes: [{notesString}]";
        }
    }
}
