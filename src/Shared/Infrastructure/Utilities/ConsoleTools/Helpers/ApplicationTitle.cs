namespace ConsoleTools.Helpers
{
    public static class Title
    {
        /// <summary>
        /// This displays the the painting music application title in the console to the user
        /// </summary>
        private static readonly string _title = @"██████╗  █████╗ ██╗███╗   ██╗████████╗██╗███╗   ██╗ ██████╗     ███╗   ███╗██╗   ██╗███████╗██╗ ██████╗
██╔══██╗██╔══██╗██║████╗  ██║╚══██╔══╝██║████╗  ██║██╔════╝     ████╗ ████║██║   ██║██╔════╝██║██╔════╝
██████╔╝███████║██║██╔██╗ ██║   ██║   ██║██╔██╗ ██║██║  ███╗    ██╔████╔██║██║   ██║███████╗██║██║     
██╔═══╝ ██╔══██║██║██║╚██╗██║   ██║   ██║██║╚██╗██║██║   ██║    ██║╚██╔╝██║██║   ██║╚════██║██║██║     
██║     ██║  ██║██║██║ ╚████║   ██║   ██║██║ ╚████║╚██████╔╝    ██║ ╚═╝ ██║╚██████╔╝███████║██║╚██████╗";

        public static void Show(string applicationTitle)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(_title);
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(applicationTitle);
            Console.WriteLine("\n\n");
            Console.ResetColor();
        }
    }
}


