using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Text.Json;

namespace Api.Function
{
    public interface ICosmosDbService
    {
        Task<Counter> GetCounterAsync();
        Task<Counter> UpdateCounterAsync(Counter counter);
    }

    public class CosmosDbService : ICosmosDbService
    {
        private readonly CosmosClient _cosmosClient;
        private readonly Container _container;
        private readonly ILogger<CosmosDbService> _logger;
        private readonly string _databaseName;
        private readonly string _containerName;

        public CosmosDbService(CosmosClient cosmosClient, IConfiguration configuration, ILogger<CosmosDbService> logger)
        {
            _cosmosClient = cosmosClient;
            _logger = logger;
            
            _databaseName = configuration["CosmosDbDatabaseName"];
            _containerName = configuration["CosmosDbContainerName"];
            
            _logger.LogInformation("Initializing CosmosDbService with database: {Database}, container: {Container}", 
                _databaseName, _containerName);
                
            _container = _cosmosClient.GetContainer(_databaseName, _containerName);
        }

        public async Task<Counter> GetCounterAsync()
        {
            try
            {
                _logger.LogInformation("Getting counter from CosmosDB");
                
                // Match the document ID in your Cosmos DB
                string counterId = "index";
                
                var response = await _container.ReadItemAsync<Counter>(
                    id: counterId,
                    partitionKey: new PartitionKey(counterId)
                );
                
                _logger.LogInformation("Counter retrieved successfully with count: {Count}", response.Resource.Count);
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                _logger.LogWarning("Counter document not found. Creating a new counter document with ID: index");
                
                // Create a new counter document if it doesn't exist
                var counter = new Counter("index", 0);
                
                // Debug: Log the JSON being sent to verify 'id' is lowercase
                string json = JsonSerializer.Serialize(counter);
                _logger.LogInformation("Creating new counter with JSON: {Json}", json);
                
                try
                {
                    var response = await _container.CreateItemAsync(counter, new PartitionKey(counter.Id));
                    _logger.LogInformation("New counter created with ID: {Id}", counter.Id);
                    return response.Resource;
                }
                catch (CosmosException createEx)
                {
                    _logger.LogError(createEx, "Error creating counter document: {Message}", createEx.Message);
                    throw;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving counter from CosmosDB: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<Counter> UpdateCounterAsync(Counter counter)
        {
            try
            {
                // Validate that Id is not null or empty
                if (string.IsNullOrEmpty(counter.Id))
                {
                    _logger.LogError("Cannot update counter with null or empty Id");
                    throw new ArgumentException("Counter Id cannot be null or empty", nameof(counter));
                }
                
                _logger.LogInformation("Updating counter in CosmosDB with ID: {Id}, Count: {Count}", counter.Id, counter.Count);
                
                // Debug: Log the JSON being sent to verify 'id' is lowercase
                string json = JsonSerializer.Serialize(counter);
                _logger.LogInformation("Updating counter with JSON: {Json}", json);
                
                // Use ReplaceItemAsync which requires the id to be set
                var response = await _container.ReplaceItemAsync(
                    item: counter,
                    id: counter.Id,
                    partitionKey: new PartitionKey(counter.Id)
                );
                
                _logger.LogInformation("Counter updated successfully");
                return response.Resource;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating counter in CosmosDB: {Message}", ex.Message);
                throw;
            }
        }
    }
}