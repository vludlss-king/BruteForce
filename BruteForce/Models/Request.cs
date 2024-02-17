using BruteForce.Contracts;

namespace BruteForce.Models
{
    internal class Request : IPassword
    {
        public string? Password { get; set; }
    }
}
