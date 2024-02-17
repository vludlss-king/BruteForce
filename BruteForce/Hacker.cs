using BruteForce.Contracts;
using Microsoft.Extensions.Logging;
using System;

namespace BruteForce
{
    internal class Hacker
    {
        private readonly ISender _sender;
        private readonly ILogger<Hacker> _logger;

        private readonly int?[] _charsIndexes;
        private readonly IReadOnlyList<char> _availableChars;

        public Hacker(ISender sender, ILogger<Hacker> logger)
        {
            _sender = sender;
            _logger = logger;

            _availableChars = Enumerable.Range(char.MinValue, char.MaxValue)
                .Select(Convert.ToChar)
                .Where(char.IsAscii)
                .Where(char.IsLetterOrDigit)
                .ToList()
                .AsReadOnly();
            _charsIndexes = new int?[16];
            _charsIndexes[0] = 0;
        }

        public void Hack()
        {
            int increaseIndex = 0;
            while (true)
            {
                var nextPassword = _charsIndexes.Where(index => index is not null).Select(index => Convert.ToString(_availableChars[index.Value]))
                    .Aggregate((previous, next) => previous + next);

                var response = _sender.Send(nextPassword);
                if(response is true)
                {
                    ClearConsoleLine();
                    Console.WriteLine($"Пароль найден {nextPassword}");
                    break;
                }
                else
                {
                    Increment(ref increaseIndex);

                    ClearConsoleLine();
                    Console.Write($"Пароль не найден {nextPassword}");
                }
            }
        }

        private void Increment(ref int index)
        {
            _charsIndexes[index]++;

            ResetPrevious(index);

            bool allMax = true;
            for (int current = index; current >= 0; current--)
            {
                if (_charsIndexes[current] < _availableChars.Count - 1)
                {
                    allMax = false;
                    break;
                }
            }
            if (allMax)
            {
                index++;
                _charsIndexes[index] = 0;
                for (int i = index - 1; i >= 0; i--)
                    _charsIndexes[i] = 0;
            }
        }

        private void ResetPrevious(int index)
        {
            var current = index;
            var previous = current - 1;

            if (_charsIndexes[current] >= _availableChars.Count)
            {
                _charsIndexes[current] = 0;

                _charsIndexes[previous]++;
                ResetPrevious(previous);
            }
        }

        public void ClearConsoleLine()
        {
            int currentLine = Console.CursorTop;
            Console.SetCursorPosition(0, currentLine);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLine);
        }
    }
}
