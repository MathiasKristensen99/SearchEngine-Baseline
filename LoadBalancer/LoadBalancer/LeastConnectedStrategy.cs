namespace LoadBalancer.LoadBalancer;

public class LeastConnectedStrategy : ILoadBalancerStrategy
{
    private int _currentServiceIndex;
    public LeastConnectedStrategy()
    {
        _currentServiceIndex = 0;
    }
    public string NextService(Dictionary<Guid, string> services)
    {
        if (services.Count == 0)
        {
            return null;
        }
        var service = services.ElementAt(_currentServiceIndex);
        _currentServiceIndex = (_currentServiceIndex + 1) % services.Count;
        return service.Value;
    }
}