using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace Company.Function;
public class MyOutputType
{
    [CosmosDBOutput(databaseName: "CloudResume", collectionName: "Counter", ConnectionStringSetting = "CosmosDbConnectionString")]
    public Counter? UpdatedCounter { get; set; }
    public HttpResponseData? HttpResponse { get; set; }
}