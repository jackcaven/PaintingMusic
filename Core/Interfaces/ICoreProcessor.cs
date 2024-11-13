using Core.DataStructures.Art;
using Core.DataStructures.Music;

namespace Core.Interfaces
{
    public interface ICoreProcessor
    {
        public IEnumerable<ImageAttributes> ImageAttributes { get; }
        public CanvasAttributes CanvasAttributes { get; }
        public IDictionary<string, MusicData> MusicData { get; }
        public void Add(ImageAttributes imageAttributes, CanvasAttributes canvasAttributes);
        public void Remove(ImageAttributes imageAttributes, CanvasAttributes canvasAttributes);
    }
}
