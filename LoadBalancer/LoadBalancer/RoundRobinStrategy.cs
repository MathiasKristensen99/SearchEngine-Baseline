namespace LoadBalancer.LoadBalancer;

public class RoundRobinStrategy : ILoadBalancerStrategy
{
    private int _currentServiceIndex;
    
    // Constructor that initializes the current service index to 0.
    public RoundRobinStrategy()
    {
        _currentServiceIndex = 0;
    }
    
    // Method that returns the next service based on round robin algorithm.
    public string NextService(Dictionary<Guid, string> services)
    {
        // If there are no services available, return null.
        if (services.Count == 0)
        {
            return null;
        }
        
        // Get the service at the current index.
        var service = services.ElementAt(_currentServiceIndex);
        
        // Increment the index and wrap around if it goes beyond the last index.
        _currentServiceIndex = (_currentServiceIndex + 1) % services.Count;
        
        // Return the URL of the service.
        return service.Value;
    }
}