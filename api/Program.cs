using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Api.Function;
using Azure.Identity;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Azure.Cosmos;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices((context, services) =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.Configure<System.Text.Json.JsonSerializerOptions>(options => 
{
    // This ensures property names are preserved during serialization
    options.PropertyNamingPolicy = null;
});
        services.AddSingleton<IVisitorCounterService, VisitorCounterService>();
        
        // Add Azure Identity DefaultAzureCredential
        // This will automatically use the most appropriate credential based on the environment:
        // - Azure CLI or Azure Developer CLI authentication when developing locally
        // - Managed Identity when deployed to Azure
        services.AddSingleton<DefaultAzureCredential>();
        
        // Configure Azure services to use DefaultAzureCredential
        services.AddAzureClients(builder =>
        {
            builder.UseCredential(provider => provider.GetRequiredService<DefaultAzureCredential>());
        });
        
        // Configure CosmosClient with DefaultAzureCredential
        var cosmosEndpoint = context.Configuration["CosmosDbEndpoint"];
        if (!string.IsNullOrEmpty(cosmosEndpoint))
        {
            services.AddSingleton(sp => 
            {
                var credential = sp.GetRequiredService<DefaultAzureCredential>();
                return new CosmosClient(cosmosEndpoint, credential);
            });
            
            // Add the CosmosDB service
            services.AddSingleton<ICosmosDbService, CosmosDbService>();
        }
    })
    .Build();

host.Run();
