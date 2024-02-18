using BruteForce.Contracts;
using BruteForce.Enums;
using BruteForce.Helpers;
using BruteForce.Impl;
using BruteForce.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
[assembly: InternalsVisibleTo("BruteForce.Tests")]

namespace BruteForce
{
    internal class Program
    {
        public static string Password = string.Empty;
        static async Task Main(string[] args)
        {
            var provider = BuildServiceProvider();
            var app = provider.GetRequiredService<Application>();

            Console.WriteLine("Введите 0, чтобы взломать консоль");
            Console.WriteLine("Введите 1, чтобы взломать Api");
            var input = Enum.Parse<HackType>(Console.ReadLine()!);
            await app.Run(input);
        }

        private static IServiceProvider BuildServiceProvider()
        {
            var services = new ServiceCollection();

            services.AddSingleton<Application>();
            services.AddSingleton<Hacker>();
            services.AddSingleton<IOutput, Output>();
            services.AddSingleton<ISender<HttpRequest, HttpResponse>, HttpSender>();
            services.AddSingleton<ISender<ConsoleRequest, ConsoleResponse>, ConsoleSender>();

            return services.BuildServiceProvider();
        }
    }
}