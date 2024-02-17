using BruteForce.Contracts;

namespace BruteForce.Impl
{
    internal class Sender : ISender
    {
        public bool Send(string password)
        {
            if (Program.Password == password)
                return true;
            else
                return false;
        }
    }
}
