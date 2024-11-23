using Core.DataStructures.Art;
using Core.DataStructures.Music;
using Core.Interfaces;
using MusicGenerationTestApplication.Utilities;
using System.Text.Json;

namespace MusicGenerationTestApplication.Pages
{
    internal class ModelInvestigation(ICoreMusicProducer coreMusicProducer)
    {
        private readonly ICoreMusicProducer coreMusicProducer = coreMusicProducer;
        private readonly List<string> actions = ["Add", "Remove", "Clear", "Quit"];
        private readonly JsonSerializerOptions jsonSerializerOptions = new()
        {
            WriteIndented = true,
        };
        private Dictionary<Guid, ObjectAttributes> attributesCache = [];

        internal void Show()
        {
            string action = string.Empty;

            while (action != "Quit")
            {
                Title.Show();
                action = Menu.Show(actions, "Please select an action: \n");

                if (action == "Add")
                {
                    Add();
                }
                else if (action == "Remove")
                {
                    Remove();
                }
                else if (action == "Clear")
                {
                    Clear();
                }
                else
                {
                    Console.WriteLine("Exiting menu");
                }

                Console.Clear();
            }


        }

        public void Add()
        {
            Title.Show();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Sending mock payload\n");
            Console.ResetColor();

            ObjectAttributes objectAttributes = MockDataProvider.GetRandomObjectAttributes();

            MusicData music = coreMusicProducer.Add(objectAttributes, 
                                                    MockDataProvider.GetRandomCanvasAttributes());

            attributesCache.Add(objectAttributes.Id, objectAttributes);

            string json = JsonSerializer.Serialize<MusicData>(music, jsonSerializerOptions);

            Console.WriteLine(json);

            Console.ReadKey();
        }

        private void Remove()
        {
            Title.Show();
            Console.ForegroundColor = ConsoleColor.Red;
            if (attributesCache.Count == 0) 
            {
                Console.WriteLine("No objects to remove");
                Console.ReadKey();
                return;
            }

            Guid coreId = Menu.Show(attributesCache.Keys.ToList(), "Please select an id to remove from Core Cache:\n");
            coreMusicProducer.Remove(attributesCache[coreId], MockDataProvider.GetRandomCanvasAttributes());

            Title.Show();
            Console.WriteLine($"Removed {coreId} from cache");
            attributesCache.Remove(coreId);
            Console.ResetColor();
            Console.ReadKey();
        }

        private void Clear()
        {
            Title.Show();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Clearing model caches");
            coreMusicProducer.Clear();
            attributesCache.Clear();
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}
