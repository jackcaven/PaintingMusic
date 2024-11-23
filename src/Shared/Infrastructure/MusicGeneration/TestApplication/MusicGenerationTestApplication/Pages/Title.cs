namespace MusicGenerationTestApplication.Pages
{
    internal static class Title
    {
        private static readonly string _title = @"██████╗  █████╗ ██╗███╗   ██╗████████╗██╗███╗   ██╗ ██████╗     ███╗   ███╗██╗   ██╗███████╗██╗ ██████╗
██╔══██╗██╔══██╗██║████╗  ██║╚══██╔══╝██║████╗  ██║██╔════╝     ████╗ ████║██║   ██║██╔════╝██║██╔════╝
██████╔╝███████║██║██╔██╗ ██║   ██║   ██║██╔██╗ ██║██║  ███╗    ██╔████╔██║██║   ██║███████╗██║██║     
██╔═══╝ ██╔══██║██║██║╚██╗██║   ██║   ██║██║╚██╗██║██║   ██║    ██║╚██╔╝██║██║   ██║╚════██║██║██║     
██║     ██║  ██║██║██║ ╚████║   ██║   ██║██║ ╚████║╚██████╔╝    ██║ ╚═╝ ██║╚██████╔╝███████║██║╚██████╗";

        public static void Show()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(_title);
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Music Generation Test Application");
            Console.WriteLine("\n\n");
            Console.ResetColor();
        }
    }
}
