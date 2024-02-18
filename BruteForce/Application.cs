using BruteForce.Contracts;
using BruteForce.Enums;
using BruteForce.Helpers;
using BruteForce.Models;

namespace BruteForce
{
    internal class Application
    {
        private readonly Hacker _hacker;
        private readonly ISender<HttpRequest, HttpResponse> _httpSender;
        private readonly ISender<ConsoleRequest, ConsoleResponse> _consoleSender;

        public Application(Hacker hacker, ISender<HttpRequest, HttpResponse> httpSender, ISender<ConsoleRequest, ConsoleResponse> consoleSender)
        {
            _hacker = hacker;
            _httpSender = httpSender;
            _consoleSender = consoleSender;
        }

        public async Task Run(HackType hackType)
        {
            switch (hackType)
            {
                case HackType.Api:
                    {
                        var request = new HttpRequest()
                        {
                            Login = "petya"
                        };
                        var result = await _hacker.Hack(_httpSender, request);
                        Console.ReadLine();
                        break;
                    }
                case HackType.Console:
                    {
                        while (true)
                        {
                            Program.Password = ReadPassword();
                            var request = new ConsoleRequest();
                            var result = await _hacker.Hack(_consoleSender, request);
                        }
                    }
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
