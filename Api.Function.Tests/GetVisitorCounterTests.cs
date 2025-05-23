using Microsoft.Extensions.Logging;
using NSubstitute;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;

namespace Api.Function.Tests.Unit
{
    public class GetVisitorCounterTests
    {
        private readonly GetVisitorCounter _sut;
        private readonly ILogger<GetVisitorCounter> _logger = NullLogger<GetVisitorCounter>.Instance;
        private readonly IVisitorCounterService _mockCounterService = Substitute.For<IVisitorCounterService>();
        private readonly ICosmosDbService _mockCosmosDbService = Substitute.For<ICosmosDbService>();

        public GetVisitorCounterTests()
        {
            _sut = new GetVisitorCounter(_logger, _mockCounterService, _mockCosmosDbService);
        }

        // Test methods...

        [Fact]
        public void IncrementCounter_ShouldIncrementCount()
        {
            // Arrange
            var service = new VisitorCounterService();
            var counter = new Counter("index", 1);

            // Act
            var updatedCounter = service.IncrementCounter(counter);

            // Assert
            updatedCounter.Count.Should().Be(2);
        }
    }
}