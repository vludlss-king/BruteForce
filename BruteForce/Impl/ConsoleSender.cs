using BruteForce.Contracts;
using BruteForce.Models;

namespace BruteForce.Impl
{
    internal class ConsoleSender : ISender<ConsoleRequest, ConsoleResponse>
    {
        public Task<ConsoleResponse> Send(ConsoleRequest request)
        {
            if (Program.Password == request.Password)
                return Task.FromResult(new ConsoleResponse() { Success = true });
            
            return Task.FromResult(new ConsoleResponse() { Success = false });
        }
    }
}
