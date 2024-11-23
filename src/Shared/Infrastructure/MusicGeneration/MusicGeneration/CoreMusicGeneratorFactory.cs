using Core.Enums.AI;
using Core.Interfaces;
using MusicGeneration.Models.Markov;

namespace MusicGeneration;

public static class CoreMusicGeneratorFactory
{
    public static ICoreMusicProducer ConstructMusicGenerator(Model model) =>
        model switch
        {
            Model.Markov => new MarkovMusicGenerator(),
            _ => throw new NotImplementedException()
        };
}
