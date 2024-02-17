using BruteForce.Contracts;
using BruteForce.Helpers;

namespace BruteForce
{
    internal class Hacker<TRequest, TResponse>
        where TRequest : IPassword
        where TResponse : ISuccess
    {
        private readonly ISender<TRequest, TResponse> _sender;

        private readonly int?[] _charsBlock;
        private readonly IReadOnlyList<char> _availableChars;

        public Hacker(ISender<TRequest, TResponse> sender)
        {
            _sender = sender;

            _availableChars = Enumerable.Range(char.MinValue, char.MaxValue)
                .Select(Convert.ToChar)
                .Where(char.IsAscii)
                .Where(char.IsLetterOrDigit)
                .ToList()
                .AsReadOnly();
            _charsBlock = new int?[16];
            _charsBlock[0] = 0;
        }

        public TResponse Hack(TRequest request)
        {
            int cursor = 0;
            while (true)
            {
                var password = GetNextPassword();

                request.Password = password;
                var response = _sender.Send(request);
                if(response.Success is true)
                {
                    Output.WriteLine($"Пароль найден {password}");
                    return response;
                }
                else
                {
                    Output.Write($"Пароль не найден {password}");
                    Increment(ref cursor);
                }
            }
        }

        private string GetNextPassword()
            => _charsBlock.Where(index => index is not null)
                .Select(index => Convert.ToString(_availableChars[index!.Value]))
                .Aggregate((previous, next) => previous + next);

        private void Increment(ref int cursor)
        {
            _charsBlock[cursor]++;

            ResetPrevious(cursor);

            bool allMax = true;
            for (int current = cursor; current >= 0; current--)
            {
                if (_charsBlock[current] < _availableChars.Count - 1)
                {
                    allMax = false;
                    break;
                }
            }
            if (allMax)
            {
                cursor++;
                _charsBlock[cursor] = 0;
                for (int i = cursor - 1; i >= 0; i--)
                    _charsBlock[i] = 0;
            }
        }

        private void ResetPrevious(int index)
        {
            var current = index;
            var previous = current - 1;

            if (_charsBlock[current] >= _availableChars.Count)
            {
                _charsBlock[current] = 0;

                _charsBlock[previous]++;
                ResetPrevious(previous);
            }
        }
    }
}
