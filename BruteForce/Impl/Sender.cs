using BruteForce.Contracts;
using BruteForce.Models;

namespace BruteForce.Impl
{
    internal class Sender : ISender<Request, Response>
    {
        public Response Send(Request request)
        {
            if (Program.Password == request.Password)
                return new() { Success = true };
            
            return new Response() { Success = false };
        }
    }
}
