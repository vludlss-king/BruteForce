using BruteForce.Contracts;

namespace BruteForce.Models
{
    internal class ConsoleRequest : IPassword
    {
        public string? Password { get; set; }
    }
}
