using System.Net;
using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Api.Function;

public class GetVisitorCounter
{
    private readonly ILogger<GetVisitorCounter> _logger;
    private readonly IVisitorCounterService _visitorCounterService;
    private readonly ICosmosDbService _cosmosDbService;

    public GetVisitorCounter(
        ILogger<GetVisitorCounter> logger, 
        IVisitorCounterService visitorCounterService,
        ICosmosDbService cosmosDbService)
    {
        _logger = logger;
        _visitorCounterService = visitorCounterService;
        _cosmosDbService = cosmosDbService;
    }

    [Function("GetVisitorCounter")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
    {
        _logger.LogInformation("GetVisitorCounter function processed a request");
        
        // Get the counter from CosmosDB
        var counter = await _cosmosDbService.GetCounterAsync();
        
        // Increment the counter
        counter = _visitorCounterService.IncrementCounter(counter);
        
        // Update the counter in CosmosDB
        await _cosmosDbService.UpdateCounterAsync(counter);

        // Return the response
        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "application/json; charset=utf-8");
        string jsonString = JsonSerializer.Serialize(counter);
        await response.WriteStringAsync(jsonString);

        return response;
    }
}
