﻿namespace BruteForce.Helpers
{
    internal static class Password
    {
        public static IReadOnlyList<char> GetAvailableChars()
            => Enumerable.Range(char.MinValue, char.MaxValue)
                .Select(Convert.ToChar)
                .Where(char.IsAsciiLetterOrDigit)
                .ToList()
                .AsReadOnly();
    }
}
