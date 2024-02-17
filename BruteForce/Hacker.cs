using BruteForce.Contracts;
using BruteForce.Helpers;

namespace BruteForce
{
    internal class Hacker<TRequest, TResponse>
        where TRequest : IPassword
        where TResponse : ISuccess, IPassword
    {
        private readonly ISender<TRequest, TResponse> _sender;
        private readonly IOutput _output;

        private readonly int?[] _passwordBlocks;
        private readonly IReadOnlyList<char> _availableChars;

        public Hacker(ISender<TRequest, TResponse> sender, IOutput output)
        {
            _sender = sender;
            _output = output;

            _availableChars = Enumerable.Range(char.MinValue, char.MaxValue)
                .Select(Convert.ToChar)
                .Where(char.IsAscii)
                .Where(char.IsLetterOrDigit)
                .ToList()
                .AsReadOnly();
            _passwordBlocks = new int?[16];
            _passwordBlocks[0] = 0;
        }

        public TResponse Hack(TRequest request, CancellationToken token = default)
        {
            int blockCursor = 0;
            while (true)
            {
                token.ThrowIfCancellationRequested();

                var password = GetNextPassword();

                request.Password = password;
                var response = _sender.Send(request);
                if(response.Success is true)
                {
                    _output.WriteLine($"Пароль найден {password}");
                    response.Password = request.Password;
                    return response;
                }
                else
                {
                    _output.Write($"Пароль не найден {password}");
                    Increment(ref blockCursor);
                }
            }
        }

        private string GetNextPassword()
            => _passwordBlocks.Where(charCursor => charCursor is not null)
                .Select(charCursor => Convert.ToString(_availableChars[charCursor!.Value]))
                .Aggregate((previous, next) => previous + next);

        private void Increment(ref int blockCursor)
        {
            _passwordBlocks[blockCursor]++;

            ResetPrevious(blockCursor);

            bool allMax = true;
            for (int currentBlockCursor = blockCursor; currentBlockCursor >= 0; currentBlockCursor--)
            {
                if (_passwordBlocks[currentBlockCursor] < _availableChars.Count - 1)
                {
                    allMax = false;
                    break;
                }
            }
            if (allMax)
            {
                blockCursor++;
                for (int currentBlockCursor = blockCursor; currentBlockCursor >= 0; currentBlockCursor--)
                    _passwordBlocks[currentBlockCursor] = 0;
            }
        }

        private void ResetPrevious(int blockCursor)
        {
            var currentBlockCursor = blockCursor;
            var previousBlockCursor = currentBlockCursor - 1;

            if (_passwordBlocks[currentBlockCursor] >= _availableChars.Count)
            {
                _passwordBlocks[currentBlockCursor] = 0;

                _passwordBlocks[previousBlockCursor]++;
                ResetPrevious(previousBlockCursor);
            }
        }
    }
}
