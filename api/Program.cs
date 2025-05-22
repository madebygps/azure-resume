using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Api.Function;
using Azure.Identity;
using Microsoft.Extensions.Azure;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddSingleton<IVisitorCounterService, VisitorCounterService>();
        
        // Add Azure Identity DefaultAzureCredential
        services.AddSingleton<DefaultAzureCredential>();
        
        // Configure Azure services to use DefaultAzureCredential
        services.AddAzureClients(builder =>
        {
            builder.UseCredential(provider => provider.GetRequiredService<DefaultAzureCredential>());
        });
    })
    .Build();

host.Run();
