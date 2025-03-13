using Core.DataStructures.Art;
using Core.DataStructures.Music;

namespace Core.BaseClasses
{
    public abstract class MusicGeneratorConfiguration
    {
        public IEnumerable<string> AvailableImageAttributes = GetAvailableAttributes(typeof(ObjectAttributes));
        public IEnumerable<string> AvailableMusicAttributes = GetAvailableAttributes(typeof(MusicAttributes));

        private static IEnumerable<string> GetAvailableAttributes(Type attributes)
        {
            foreach (var item in attributes.GetProperties())
            {
                yield return item.Name;
            }
        }
    }
}
