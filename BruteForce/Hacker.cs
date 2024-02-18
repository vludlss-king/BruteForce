using BruteForce.Contracts;
using BruteForce.Helpers;

namespace BruteForce
{
    internal class Hacker
    {
        private readonly IOutput _output;

        private readonly int?[] _passwordBlocks;
        private readonly IReadOnlyList<char> _availableChars;

        public Hacker(IOutput output)
        {
            _output = output;

            _availableChars = Password.GetAvailableChars();
            _passwordBlocks = new int?[16];
            _passwordBlocks[0] = 0;
        }

        public async Task<TResponse> Hack<TRequest, TResponse>(ISender<TRequest, TResponse> sender, TRequest request, CancellationToken token = default)
            where TRequest : IPassword
            where TResponse : ISuccess, IPassword
        {
            int blockCursor = 0;
            while (true)
            {
                token.ThrowIfCancellationRequested();

                var password = GetNextPassword();

                request.Password = password;
                var response = await sender.Send(request);
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
