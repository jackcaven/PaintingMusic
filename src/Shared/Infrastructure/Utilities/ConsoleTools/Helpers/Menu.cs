using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTools.Helpers
{
    public static class Menu
    {
        /// <summary>
        /// Displays a menu and allows the user to select an option using arrow keys.
        /// </summary>
        /// <typeparam name="T">The type of the options in the menu.</typeparam>
        /// <param name="options">The list of options to display.</param>
        /// <param name="prompt">The prompt to display above the menu.</param>
        /// <returns>The selected item of type T.</returns>
        public static T Show<T>(List<T> options, string prompt)
        {
            if (options == null || options.Count == 0)
                throw new ArgumentException("Options list cannot be null or empty.");

            int selectedIndex = 0;

            while (true)
            {
                Console.Clear();
                Console.WriteLine(prompt);

                // Display the menu
                for (int i = 0; i < options.Count; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"> {options[i]}");
                    }
                    else
                    {
                        Console.ResetColor();
                        Console.WriteLine($"  {options[i]}");
                    }
                }

                // Read key input for navigation
                ConsoleKey key = Console.ReadKey(true).Key;

                // Handle key presses
                if (key == ConsoleKey.UpArrow)
                {
                    selectedIndex = (selectedIndex - 1 + options.Count) % options.Count;
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    selectedIndex = (selectedIndex + 1) % options.Count;
                }
                else if (key == ConsoleKey.Enter)
                {
                    Console.ResetColor();
                    Console.Clear();
                    return options[selectedIndex];
                }
            }
        }
    }
}
