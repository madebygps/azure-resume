using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Api.Function
{
    public interface ICosmosDbService
    {
        Task<Counter> IncrementCounterAsync();
    }

    public class CosmosDbService : ICosmosDbService
    {
        private readonly Container _container;
        private readonly ILogger<CosmosDbService> _logger;
        private const string COUNTER_ID = "visitor-count";

        public CosmosDbService(CosmosClient cosmosClient, IConfiguration configuration, ILogger<CosmosDbService> logger)
        {
            _logger = logger;
            var databaseName = configuration["CosmosDbDatabaseName"];
            var containerName = configuration["CosmosDbContainerName"];
            _container = cosmosClient.GetContainer(databaseName, containerName);
        }

        public async Task<Counter> IncrementCounterAsync()
        {
            try
            {
                var patchOperations = new[] 
                { 
                    PatchOperation.Increment("/count", 1),
                    PatchOperation.Set("/lastUpdated", DateTime.UtcNow)
                };
                
                var response = await _container.PatchItemAsync<Counter>(
                    id: COUNTER_ID,
                    partitionKey: new PartitionKey(COUNTER_ID),
                    patchOperations: patchOperations
                );

                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                var counter = new Counter(COUNTER_ID, 1);
                var response = await _container.CreateItemAsync(counter, new PartitionKey(COUNTER_ID));
                return response.Resource;
            }
        }
    }
}