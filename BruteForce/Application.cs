using BruteForce.Contracts;
using BruteForce.Enums;
using BruteForce.Helpers;
using BruteForce.HttpClients;
using BruteForce.Models;
using Newtonsoft.Json;

namespace BruteForce
{
    internal class Application
    {
        private readonly Hacker _hacker;
        private readonly ISender<HttpRequest, HttpResponse> _httpSender;
        private readonly ISender<ConsoleRequest, ConsoleResponse> _consoleSender;
        private readonly IApiHttpClient _apiHttpClient;

        public Application(Hacker hacker,
            ISender<HttpRequest, HttpResponse> httpSender,
            ISender<ConsoleRequest, ConsoleResponse> consoleSender,
            IApiHttpClient apiHttpClient)
        {
            _hacker = hacker;
            _httpSender = httpSender;
            _consoleSender = consoleSender;
            _apiHttpClient = apiHttpClient;
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

                        if (result.Success)
                        {
                            var response = await _apiHttpClient.GetPassportData();
                        
                            Console.WriteLine($"Вы успешно вошли в аккаунт! Вот паспортные данные пользователя: {JsonConvert.SerializeObject(response.Content)}");
                            Console.ReadLine();
                        }
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
