namespace BruteForce.Helpers
{
    internal static class Password
    {
        public static IReadOnlyList<char> GetAvailableChars()
            => Enumerable.Range(char.MinValue, char.MaxValue)
                .Select(Convert.ToChar)
                .Where(char.IsAsciiLetterOrDigit)
                .ToList()
                .AsReadOnly();

        public static bool IsValid(string password)
        {
            var chars = GetAvailableChars();

            return password.All(@char => chars.Contains(@char));
        }
    }
}
