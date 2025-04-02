using Core.DataStructures.Music;

namespace Core.DataStructures.Result
{
    public class CoreResult
    {
        public string ModelDecisionLogic { get; set; } = string.Empty;

        public required MusicData MusicData { get; set; }
    }
}
