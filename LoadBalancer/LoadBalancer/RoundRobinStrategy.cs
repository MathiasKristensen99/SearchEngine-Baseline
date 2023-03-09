namespace LoadBalancer.LoadBalancer;

public class RoundRobinStrategy : ILoadBalancerStrategy
{
    private readonly Dictionary<Guid, string> _services;
    private int _currentServiceIndex;

    public RoundRobinStrategy(Dictionary<Guid, string> services)
    {
        _services = services;
        _currentServiceIndex = 0;
    }

    public string NextService()
    {
        if (_services.Count == 0)
        {
            return null;
        }
        
        var service = _services.ElementAt(_currentServiceIndex);
        _currentServiceIndex = (_currentServiceIndex + 1) % _services.Count;
        return service.Value;
    }
}