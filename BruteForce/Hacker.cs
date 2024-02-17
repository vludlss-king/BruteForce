using BruteForce.Contracts;
using BruteForce.Helpers;

namespace BruteForce
{
    internal class Hacker<TRequest, TResponse>
        where TRequest : IPassword
        where TResponse : ISuccess
    {
        private readonly ISender<TRequest, TResponse> _sender;

        private readonly int?[] _charsIndexes;
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
            _charsIndexes = new int?[16];
            _charsIndexes[0] = 0;
        }

        public TResponse Hack(TRequest request)
        {
            int incrementableIndex = 0;
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
                    Increment(ref incrementableIndex);
                }
            }
        }

        private string GetNextPassword()
            => _charsIndexes.Where(index => index is not null)
                .Select(index => Convert.ToString(_availableChars[index!.Value]))
                .Aggregate((previous, next) => previous + next);

        private void Increment(ref int index)
        {
            _charsIndexes[index]++;

            ResetPrevious(index);

            bool allMax = true;
            for (int current = index; current >= 0; current--)
            {
                if (_charsIndexes[current] < _availableChars.Count - 1)
                {
                    allMax = false;
                    break;
                }
            }
            if (allMax)
            {
                index++;
                _charsIndexes[index] = 0;
                for (int i = index - 1; i >= 0; i--)
                    _charsIndexes[i] = 0;
            }
        }

        private void ResetPrevious(int index)
        {
            var current = index;
            var previous = current - 1;

            if (_charsIndexes[current] >= _availableChars.Count)
            {
                _charsIndexes[current] = 0;

                _charsIndexes[previous]++;
                ResetPrevious(previous);
            }
        }
    }
}
