using Core.BaseClasses;
using Core.DataStructures.Art;
using Core.DataStructures.Music;
using Core.Interfaces;
using MusicGeneration.Models.Som.Configuration;
using System.ComponentModel;

namespace MusicGeneration.Models.Som
{
    class SomMusicGenerator : ICoreMusicProducer
    {
        private SomConfiguration? configuration;
        private Dictionary<string, MusicData> musicDataCache = [];
        private List<ObjectAttributes> objectAttributesCache = [];
        private CanvasAttributes? canvasAttributesCache;

        public void Configure(MusicGeneratorConfiguration configuration)
        {
            if (configuration is SomConfiguration config)
            {
                this.configuration = config;
            }
            else
            {
                throw new InvalidEnumArgumentException($"Invalid configuration was supplied to {nameof(SomMusicGenerator)}");
            }
        }

        public MusicData Add(ObjectAttributes imageAttributes, CanvasAttributes canvasAttributes)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            musicDataCache.Clear();
            objectAttributesCache.Clear();
            canvasAttributesCache = null;
        }

        public void Remove(ObjectAttributes imageAttributes, CanvasAttributes canvasAttributes)
        {
            canvasAttributesCache = canvasAttributes;
            objectAttributesCache.Remove(imageAttributes);
            musicDataCache.Remove(imageAttributes.Id);
        }
    }
}
