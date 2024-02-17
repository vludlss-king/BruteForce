using BruteForce.Helpers;
using BruteForce.Impl;
using BruteForce.Models;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
[assembly: InternalsVisibleTo("BruteForce.Tests")]

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

                var hacker = new Hacker<Request, Response>(new Sender(), new Output());
                var request = new Request();
                var result = hacker.Hack(request);
            }
        }

        private static string ReadPassword()
        {
            var password = string.Empty;
            do
            {
                Console.Write("Введите ваш пароль: ");
                password = Console.ReadLine();

                if (!Helpers.Password.IsValid(password))
                {
                    Console.WriteLine();
                    Console.WriteLine("Пароль имеет неверный формат, введите ещё раз");
                    continue;
                }
                else break;
            } while (true);
            return password;
        }
    }
}