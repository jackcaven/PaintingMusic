using Core.DataStructures.Art;

namespace MusicGenerationTestApplication.Utilities
{
    internal static class MockDataProvider
    {
        internal static Random Random = new();
        internal static ObjectAttributes GetRandomObjectAttributes()
        {
            return new ObjectAttributes()
            {
                Id = Guid.NewGuid(),
                ColorR = Random.NextDouble(),
                ColorB = Random.NextDouble(),
                ColorG = Random.NextDouble(),
                Temperature = Random.NextDouble(),
                Hue = Random.NextDouble(),
                Tone = Random.NextDouble(),
                Area = Random.NextDouble(),
                Complexity = Random.NextDouble(),
                CanvasLocation = (Random.NextDouble(), Random.NextDouble()),
                COG = (Random.NextDouble(), Random.NextDouble())
            };
        }

        internal static CanvasAttributes GetRandomCanvasAttributes()
        {
            return new CanvasAttributes()
            {
                AreaCovered = Random.NextDouble(),
                COG = (Random.NextDouble(), Random.NextDouble())
            };
        }
    }
}
