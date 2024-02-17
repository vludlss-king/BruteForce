namespace BruteForce.Helpers
{
    internal static class Output
    {
        public static void ClearLine()
        {
            int currentLine = Console.CursorTop;
            Console.SetCursorPosition(0, currentLine);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLine);
        }

        public static void Write(string message)
        {
            ClearLine();
            Console.Write(message);
        }

        public static void WriteLine(string message)
        {
            ClearLine();
            Console.WriteLine(message);
        }
    }
}
