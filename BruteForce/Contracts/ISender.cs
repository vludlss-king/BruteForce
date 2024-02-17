namespace BruteForce.Contracts
{
    internal interface ISender<TRequest, TResponse>
        where TRequest : IPassword
        where TResponse : ISuccess
    {
        public TResponse Send(TRequest request);
    }
}
