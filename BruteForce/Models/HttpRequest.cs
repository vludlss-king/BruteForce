using BruteForce.Contracts;

namespace BruteForce.Models
{
    internal class HttpRequest : IPassword
    {
        public string Login { get; set; }
        public string? Password { get; set; }
    }
}
