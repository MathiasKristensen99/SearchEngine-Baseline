namespace LoadBalancer.LoadBalancer;

public class LoadBalancer : ILoadBalancer
{
    private static ILoadBalancer instance;
    private ILoadBalancerStrategy _roundRobinStrategy;
    private ILoadBalancerStrategy _activeStrategy;
    private Dictionary<Guid, string> _services;

    private LoadBalancer()
    {
        _services = new Dictionary<Guid, string>();
        _roundRobinStrategy = new RoundRobinStrategy();
        // Default strategy - Round Robin
        _activeStrategy = _roundRobinStrategy;
    }

    public static ILoadBalancer getInstance()
    {
        if (instance == null)
        {
            instance = new LoadBalancer();
        }
        return instance;
    }
    public Dictionary<Guid, string> GetAllServices()
    {
        return _services;
    }

    public Guid AddService(string url)
    {
        var id = Guid.NewGuid();
        _services.Add(id, url);
        return id;
    }

    public Guid RemoveService(Guid id)
    {
        _services.Remove(id);
        return id;
    }

    public ILoadBalancerStrategy GetActiveStrategy()
    {
        throw new NotImplementedException();
    }

    public void SetActiveStrategy(ILoadBalancerStrategy strategy)
    {
        _activeStrategy = strategy;
    }

    public string NextService()
    {
        return _activeStrategy.NextService(GetAllServices());
    }
    
    public void UseRoundRobinStrategy()
    {
        // Set the active strategy to the round-robin strategy.
        _activeStrategy = _roundRobinStrategy;
    }
}