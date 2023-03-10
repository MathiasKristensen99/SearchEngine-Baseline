namespace LoadBalancer.LoadBalancer;

public class LoadBalancer : ILoadBalancer
{
    private static ILoadBalancer instance;
    private ILoadBalancerStrategy _roundRobinStrategy;
    private ILoadBalancerStrategy _leastConnectedStrategy;
    private ILoadBalancerStrategy _activeStrategy;
    private Dictionary<Guid, string> _services;
    private List<ILoadBalancerStrategy> _allStrategies = new ();

    private LoadBalancer()
    {
        _services = new Dictionary<Guid, string>();
        _roundRobinStrategy = new RoundRobinStrategy();
        _leastConnectedStrategy = new LeastConnectedStrategy();
        // Default strategy - Round Robin
        _activeStrategy = _leastConnectedStrategy;
        _allStrategies.Add(_roundRobinStrategy);
        _allStrategies.Add(_leastConnectedStrategy);
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
        return _activeStrategy;
    }

    public void SetActiveStrategy(int selection)
    {
        if (selection == 1)
        {
            UseLeastConnectedStrategy();
        }
        if (selection == 2)
        {
            UseRoundRobinStrategy();
            
        }
    }

    public string NextService()
    {
        var nextService = _activeStrategy.NextService(GetAllServices());
        Console.WriteLine(nextService);
        return nextService;
    }
    
    private void UseRoundRobinStrategy()
    {
        // Set the active strategy to the round-robin strategy.
        _activeStrategy = _roundRobinStrategy;
    }

    private void UseLeastConnectedStrategy()
    {
        // Set the active strategy to the least-connected strategy.
        _activeStrategy = _leastConnectedStrategy;
    }
}