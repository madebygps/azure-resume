using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;

namespace Api.Function;

public interface ICosmosDbService
{
    Task<Counter> GetCounterAsync();
    Task<Counter> UpdateCounterAsync(Counter counter);
}

public class CosmosDbService : ICosmosDbService
{
    private readonly CosmosClient _cosmosClient;
    private readonly ILogger<CosmosDbService> _logger;
    private readonly Container _container;
    
    private const string DatabaseId = "CloudResume";
    private const string ContainerId = "Counter";
    private const string CounterId = "index";
    private const string PartitionKey = "index";
    
    public CosmosDbService(CosmosClient cosmosClient, ILogger<CosmosDbService> logger)
    {
        _cosmosClient = cosmosClient ?? throw new ArgumentNullException(nameof(cosmosClient));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        
        // Get a reference to the container
        _container = _cosmosClient.GetContainer(DatabaseId, ContainerId);
    }
    
    public async Task<Counter> GetCounterAsync()
    {
        try
        {
            // Try to read the counter
            ItemResponse<Counter> response = await _container.ReadItemAsync<Counter>(
                id: CounterId,
                partitionKey: new PartitionKey(PartitionKey)
            );
            
            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            // If the counter doesn't exist, create it
            _logger.LogInformation("Counter not found, creating new counter");
            var counter = new Counter(CounterId, 0);
            await _container.CreateItemAsync(counter, new PartitionKey(PartitionKey));
            return counter;
        }
    }
    
    public async Task<Counter> UpdateCounterAsync(Counter counter)
    {
        // Update the counter in the database
        ItemResponse<Counter> response = await _container.UpsertItemAsync(
            counter,
            new PartitionKey(PartitionKey)
        );
        
        return response.Resource;
    }
}