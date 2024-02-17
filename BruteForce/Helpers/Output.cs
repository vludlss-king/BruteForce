using BruteForce.Contracts;

namespace BruteForce.Helpers
{
    internal class Output : IOutput
    {
        public void Write(string message)
        {
            ClearLine();
            Console.Write(message);
        }

        public void WriteLine(string message)
        {
            ClearLine();
            Console.WriteLine(message);
        }

        private void ClearLine()
        {
            int currentLine = Console.CursorTop;
            Console.SetCursorPosition(0, currentLine);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLine);
        }
    }
}
