using BruteForce.Contracts;
using BruteForce.Helpers;
using BruteForce.Impl;
using BruteForce.Models;

namespace BruteForce.Tests
{
    [Collection(nameof(HackerTests))]
    public class HackerTests
    {
        const int _timeoutInSeconds = 5;

        public static IEnumerable<object[]> GetAvailableChars()
            => Password.GetAvailableChars().Select(@char => new object[] { @char }).ToList();

        [Theory]
        [MemberData(nameof(GetAvailableChars))]
        public async Task Latin_password_from_available_character_was_hacked(string @char)
        {
            // Arrange
            Program.Password = @char;

            var outputStub = new Mock<IOutput>();
            var hacker = new Hacker<ConsoleRequest, ConsoleResponse>(new ConsoleSender(), outputStub.Object);

            var request = new ConsoleRequest();
            var tokenSource = new CancellationTokenSource();
            tokenSource.CancelAfter((int)TimeSpan.FromSeconds(_timeoutInSeconds).TotalMilliseconds);

            // Act
            var result = await hacker.Hack(request, tokenSource.Token);

            // Assert
            result.Success.Should().Be(true);
            result.Password.Should().Be(Program.Password);
        }
    }

    [CollectionDefinition(nameof(HackerTests), DisableParallelization = false)]
    public class HackerTestsCollection
    {

    }
}