using BruteForce.Contracts;

namespace BruteForce.Models
{
    internal class Response : ISuccess, IPassword
    {
        public bool Success { get; set; }
        public string? Password { get; set; }
    }
}
