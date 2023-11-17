using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;


namespace Api.Function;
public class MyOutputType
{


    [CosmosDBOutput("CloudResume", "Counter", Connection = "CosmosDbConnectionString")]
    public Counter? UpdatedCounter { get; set; }
    public HttpResponseData? HttpResponse { get; set; }
}