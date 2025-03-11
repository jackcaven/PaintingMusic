using Core.BaseClasses;
using Core.DataStructures.Art;
using Core.DataStructures.Music;

namespace Core.Interfaces
{
    public interface ICoreMusicProducer
    {
        public void Configure(MusicGeneratorConfiguration configuration);
        public MusicData Add(ObjectAttributes imageAttributes, CanvasAttributes canvasAttributes);
        public void Remove(ObjectAttributes imageAttributes, CanvasAttributes canvasAttributes);
        public void Clear();
    }
}
