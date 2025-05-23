using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Api.Function
{
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
            
            try
            {
                // Get the counter from CosmosDB
                var counter = await _cosmosDbService.GetCounterAsync();
                
                // Debug: Log the counter JSON to ensure 'id' property exists
                var counterJson = JsonSerializer.Serialize(counter);
                _logger.LogInformation("Counter before increment: {Json}", counterJson);
                
                // Check if Id is set
                if (string.IsNullOrEmpty(counter.Id))
                {
                    _logger.LogWarning("Counter retrieved with null or empty Id. Setting Id to 'index'");
                    counter.Id = "index";
                }
                
                // Increment the counter
                counter = _visitorCounterService.IncrementCounter(counter);
                
                // Debug: Log the counter JSON again after increment
                counterJson = JsonSerializer.Serialize(counter);
                _logger.LogInformation("Counter after increment: {Json}", counterJson);
                
                // Update the counter in CosmosDB
                await _cosmosDbService.UpdateCounterAsync(counter);

                // Return the response
                var response = req.CreateResponse(HttpStatusCode.OK);
                response.Headers.Add("Content-Type", "application/json; charset=utf-8");
                response.Headers.Add("Access-Control-Allow-Origin", "*");
                
                // Configure JSON options to maintain property casing
                var options = new JsonSerializerOptions { 
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                };
                
                await response.WriteStringAsync(JsonSerializer.Serialize(counter, options));
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing counter request: {Message}", ex.Message);
                
                var response = req.CreateResponse(HttpStatusCode.InternalServerError);
                response.Headers.Add("Content-Type", "application/json; charset=utf-8");
                response.Headers.Add("Access-Control-Allow-Origin", "*");
                
                var error = new { error = $"Error processing request: {ex.Message}" };
                await response.WriteStringAsync(JsonSerializer.Serialize(error));
                
                return response;
            }
        }
    }
}