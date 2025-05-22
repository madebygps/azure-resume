using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;


namespace Api.Function;
public class UpdatedCounter
{

    [CosmosDBOutput("CloudResume", "Counter", Connection = "")]
    public Counter? NewCounter { get; set; }
    public HttpResponseData? HttpResponse { get; set; }
}