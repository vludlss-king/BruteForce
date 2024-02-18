using BruteForce.Contracts;

namespace BruteForce.Models
{
    internal class HttpResponse : ISuccess, IPassword
    {
        public bool Success { get; set; }
        public string? Password { get; set; }
    }
}
