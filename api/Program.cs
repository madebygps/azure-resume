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
        
        // Configure JSON serialization for your Counter model
        services.Configure<System.Text.Json.JsonSerializerOptions>(options => 
        {
            // Use camelCase for consistency with your Counter model JsonPropertyName attributes
            options.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
            options.WriteIndented = false; // Optimize for performance
        });
        
        // REMOVE this line - no longer needed:
        // services.AddSingleton<IVisitorCounterService, VisitorCounterService>();
        
        // Add Azure Identity DefaultAzureCredential
        services.AddSingleton<DefaultAzureCredential>();
        
        // Configure Azure services to use DefaultAzureCredential
        services.AddAzureClients(builder =>
        {
            builder.UseCredential(provider => provider.GetRequiredService<DefaultAzureCredential>());
        });
        
        // Configure CosmosClient with DefaultAzureCredential and performance optimizations
        var cosmosEndpoint = context.Configuration["CosmosDbEndpoint"];
        if (!string.IsNullOrEmpty(cosmosEndpoint))
        {
            services.AddSingleton(sp => 
            {
                var credential = sp.GetRequiredService<DefaultAzureCredential>();
                var cosmosClientOptions = new CosmosClientOptions
                {
                    ConnectionMode = ConnectionMode.Direct, // Better performance
                    ConsistencyLevel = ConsistencyLevel.Session, // Balanced performance/consistency
                    RequestTimeout = TimeSpan.FromSeconds(10),
                    MaxRetryAttemptsOnRateLimitedRequests = 3
                };
                
                return new CosmosClient(cosmosEndpoint, credential, cosmosClientOptions);
            });
            
            // Add the CosmosDB service - this is the only service you need now
            services.AddSingleton<ICosmosDbService, CosmosDbService>();
        }
    })
    .Build();

host.Run();