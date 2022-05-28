using System.Net;
using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Company.Function
{
    public class GetVisitorCounter
    {
        private readonly ILogger _logger;

        public GetVisitorCounter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<GetVisitorCounter>();
        }

        [Function("GetVisitorCounter")]
        public MyOutputType Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req, 
        [CosmosDBInput(databaseName: "CloudResume", collectionName: "Counter", ConnectionStringSetting = "CosmosDbConnectionString", Id = "index",
                PartitionKey = "index")] Counter input)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            
            response.Headers.Add("Content-Type", "application/json; charset=utf-8");

            string jsonString = JsonSerializer.Serialize(input);

            response.WriteString(jsonString);
            // custom output type
            return new MyOutputType()
            {
                Counter = new Counter
                {
                    Id = "index",
                    Count = input.Count + 1
                },

                HttpResponse = response
            };
        }
    }


    public class MyOutputType
    {
        [CosmosDBOutput(databaseName: "CloudResume", collectionName: "Counter", ConnectionStringSetting = "CosmosDbConnectionString")]
        public Counter Counter { get; set; }
        public HttpResponseData HttpResponse { get; set; }
    }
}
