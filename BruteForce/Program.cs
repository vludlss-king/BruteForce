using BruteForce.Impl;
using BruteForce.Models;

namespace BruteForce
{
    internal class Program
    {
        public static string Password = string.Empty;
        static void Main(string[] args)
        {
            while (true)
            {
                Password = ReadPassword();

                var hacker = new Hacker<Request, Response>(new Sender());
                var request = new Request();
                var result = hacker.Hack(request);

                Console.ReadLine();
            }
        }

        private static string ReadPassword()
        {
            var password = string.Empty;
            do
            {
                Console.Write("Введите ваш пароль: ");
                password = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(password))
                {
                    Console.WriteLine();
                    Console.Write("Пароль пустой, введите ещё раз: ");
                    continue;
                }

                if (!password.All(@char => char.IsAscii(@char) && char.IsLetterOrDigit(@char)))
                {
                    Console.WriteLine();
                    Console.Write("Пароль имеет неверный формат, введите ещё раз");
                    continue;
                }

                if (!string.IsNullOrWhiteSpace(password) && password.All(@char => char.IsAscii(@char) && char.IsLetterOrDigit(@char)))
                    break;
            } while (true);
            return password;
        }
    }
}