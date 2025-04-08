using Core.BaseClasses;
using Core.DataStructures.Art;
using Core.DataStructures.Result;

namespace Core.Interfaces
{
    public interface ICoreMusicProducer
    {
        public CoreResult Add(ObjectAttributes imageAttributes, CanvasAttributes canvasAttributes);
        public void Configure(MusicGeneratorConfiguration configuration);
        public void Remove(ObjectAttributes imageAttributes, CanvasAttributes canvasAttributes);
        public void Clear();
    }
}
