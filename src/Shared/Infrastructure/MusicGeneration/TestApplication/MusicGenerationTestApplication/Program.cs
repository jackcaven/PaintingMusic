using Core.Enums.AI;
using Core.Interfaces;
using MusicGeneration;
using MusicGenerationTestApplication.Pages;

Model selectedModel = Menu.Show(Enum.GetValues<Model>().ToList(),
                                "Please select a model to test:\n");

// Model Selection
Console.Title = "Painting Music - Test Application";
Title.Show();
Console.WriteLine($"Loading {selectedModel} model...");
ICoreMusicProducer musicProducer = CoreMusicGeneratorFactory.ConstructMusicGenerator(selectedModel);
Console.Clear();

// Interact with model
ModelInvestigation investigate = new(musicProducer);
investigate.Show();