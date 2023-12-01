using Api.Function;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Moq;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

public class GetVisitorCounterTests
{
   private readonly ILogger<GetVisitorCounter> logger;
    private readonly Mock<HttpRequestData> mockRequest;
    private readonly Mock<FunctionContext> mockFunctionContext;

    public GetVisitorCounterTests()
    {
        logger = Mock.Of<ILogger<GetVisitorCounter>>();
        mockFunctionContext = new Mock<FunctionContext>();
        mockRequest = new Mock<HttpRequestData>(MockBehavior.Strict, mockFunctionContext.Object);

        mockRequest.Setup(req => req.CreateResponse()).Returns(() => CreateMockHttpResponseData(mockFunctionContext.Object));
    }


    [Fact]
    public async Task Run_ShouldIncrementCounter()
    {
        // Arrange
        var counter = new Counter(id : "index"){ Count = 1 };
        var function = new GetVisitorCounter(logger);
        var loggerFactoryMock = new Mock<ILoggerFactory>();
        var loggerMock = new Mock<ILogger>();
        loggerFactoryMock.Setup(x => x.CreateLogger(It.IsAny<string>())).Returns(loggerMock.Object);
        var requestMock = new Mock<HttpRequestData>();
        var responseMock = new Mock<HttpResponseData>();

        // Simulate the function call
            var response = function.Run(requestMock.Object, counter);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            // Verify that the response contains the expected content type
            Assert.True(response.Headers.TryGetValues("Content-Type", out var contentTypes));
            Assert.Contains("text/plain; charset=utf-8", contentTypes);

        // Act
        var result = await function.Run(mockRequest.Object, counter);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.NewCounter.Count); // Check if the counter is incremented
        // Additional assertions as necessary
    }
}

// Arrange
            
            

            

            

            



