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
        private readonly ICosmosDbService _cosmosDbService;

        public GetVisitorCounter(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        [Function("GetVisitorCounter")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            try
            {
                var counter = await _cosmosDbService.IncrementCounterAsync();

                var response = req.CreateResponse(HttpStatusCode.OK);
                response.Headers.Add("Content-Type", "application/json");
                response.Headers.Add("Access-Control-Allow-Origin", "*");
                
                await response.WriteStringAsync(JsonSerializer.Serialize(new { count = counter.Count }));
                return response;
            }
            catch (Exception)
            {
                var response = req.CreateResponse(HttpStatusCode.InternalServerError);
                await response.WriteStringAsync(JsonSerializer.Serialize(new { error = "Unable to get visitor count" }));
                return response;
            }
        }
    }
}