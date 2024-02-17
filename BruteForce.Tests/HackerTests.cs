using BruteForce.Contracts;
using BruteForce.Impl;
using BruteForce.Models;

namespace BruteForce.Tests
{
    [Collection(nameof(HackerTests))]
    public class HackerTests
    {
        const int _timeoutInSeconds = 5;

        [Fact]
        public void Latin_password_from_a_character_was_hacked()
        {
            // Arrange
            Program.Password = "a";

            var outputStub = new Mock<IOutput>();
            var hacker = new Hacker<Request, Response>(new Sender(), outputStub.Object);

            var request = new Request();
            var tokenSource = new CancellationTokenSource();
            tokenSource.CancelAfter((int)TimeSpan.FromSeconds(_timeoutInSeconds).TotalMilliseconds);

            // Act
            var result = hacker.Hack(request, tokenSource.Token);

            // Assert
            result.Success.Should().Be(true);
            result.Password.Should().Be(Program.Password);
        }

        [Fact]
        public void Latin_password_from_z_character_was_hacked()
        {
            // Arrange
            Program.Password = "z";

            var outputStub = new Mock<IOutput>();
            var hacker = new Hacker<Request, Response>(new Sender(), outputStub.Object);

            var request = new Request();
            var tokenSource = new CancellationTokenSource();
            tokenSource.CancelAfter((int)TimeSpan.FromSeconds(_timeoutInSeconds).TotalMilliseconds);

            // Act
            var result = hacker.Hack(request, tokenSource.Token);

            // Assert
            result.Success.Should().Be(true);
            result.Password.Should().Be(Program.Password);
        }

        [Fact]
        public void Latin_password_from_A_character_was_hacked()
        {
            // Arrange
            Program.Password = "A";

            var outputStub = new Mock<IOutput>();
            var hacker = new Hacker<Request, Response>(new Sender(), outputStub.Object);

            var request = new Request();
            var tokenSource = new CancellationTokenSource();
            tokenSource.CancelAfter((int)TimeSpan.FromSeconds(_timeoutInSeconds).TotalMilliseconds);

            // Act
            var result = hacker.Hack(request, tokenSource.Token);

            // Assert
            result.Success.Should().Be(true);
            result.Password.Should().Be(Program.Password);
        }

        [Fact]
        public void Latin_password_from_Z_character_was_hacked()
        {
            // Arrange
            Program.Password = "Z";

            var outputStub = new Mock<IOutput>();
            var hacker = new Hacker<Request, Response>(new Sender(), outputStub.Object);

            var request = new Request();
            var tokenSource = new CancellationTokenSource();
            tokenSource.CancelAfter((int)TimeSpan.FromSeconds(_timeoutInSeconds).TotalMilliseconds);

            // Act
            var result = hacker.Hack(request, tokenSource.Token);

            // Assert
            result.Success.Should().Be(true);
            result.Password.Should().Be(Program.Password);
        }

        [Fact]
        public void Latin_password_from_1_character_was_hacked()
        {
            // Arrange
            Program.Password = "1";

            var outputStub = new Mock<IOutput>();
            var hacker = new Hacker<Request, Response>(new Sender(), outputStub.Object);

            var request = new Request();
            var tokenSource = new CancellationTokenSource();
            tokenSource.CancelAfter((int)TimeSpan.FromSeconds(_timeoutInSeconds).TotalMilliseconds);

            // Act
            var result = hacker.Hack(request, tokenSource.Token);

            // Assert
            result.Success.Should().Be(true);
            result.Password.Should().Be(Program.Password);
        }

        [Fact]
        public void Latin_password_from_0_character_was_hacked()
        {
            // Arrange
            Program.Password = "0";

            var outputStub = new Mock<IOutput>();
            var hacker = new Hacker<Request, Response>(new Sender(), outputStub.Object);

            var request = new Request();
            var tokenSource = new CancellationTokenSource();
            tokenSource.CancelAfter((int)TimeSpan.FromSeconds(_timeoutInSeconds).TotalMilliseconds);

            // Act
            var result = hacker.Hack(request, tokenSource.Token);

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