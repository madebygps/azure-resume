using Microsoft.Extensions.Logging;

namespace Api.Function
{
    public interface IVisitorCounterService
    {
        Counter IncrementCounter(Counter counter);
    }

    public class VisitorCounterService : IVisitorCounterService
    {
        private readonly ILogger<VisitorCounterService> _logger;

        public VisitorCounterService(ILogger<VisitorCounterService> logger)
        {
            _logger = logger;
        }

        public Counter IncrementCounter(Counter counter)
        {
            _logger.LogInformation("Incrementing counter from {CurrentCount}", counter.Count);
            
            // Ensure Id is preserved
            if (string.IsNullOrEmpty(counter.Id))
            {
                counter.Id = "index";
                _logger.LogWarning("Counter Id was null or empty, set to 'index'");
            }
            
            // Increment the count directly on the existing object
            counter.Count++;
            
            return counter;
        }
    }
}