namespace LoadBalancer.LoadBalancer;

public class LoadBalancer : ILoadBalancer
{
    private static ILoadBalancer instance;
    private ILoadBalancerStrategy _roundRobinStrategy;
    private ILoadBalancerStrategy _leastConnectedStrategy;
    private ILoadBalancerStrategy _activeStrategy;
    private Dictionary<Guid, string> _services;
    private List<ILoadBalancerStrategy> _allStrategies = new();

    /**
     * Private constructor to prevent direct instantiation.
     * Initializes the dictionary of services and adds the available load balancing               
       strategies.
     * Sets the default active strategy to be the least connected strategy.
     */
    private LoadBalancer()
    {
        _services = new Dictionary<Guid, string>();
        _roundRobinStrategy = new RoundRobinStrategy();
        _leastConnectedStrategy = new LeastConnectedStrategy(_services);
        // Default strategy - Round Robin
        _activeStrategy = _roundRobinStrategy;
        _allStrategies.Add(_roundRobinStrategy);
        _allStrategies.Add(_leastConnectedStrategy);
    }

    /**
     * Returns the singleton instance of the LoadBalancer class.
     *
     * @return The singleton instance of the LoadBalancer class.
     */
    public static ILoadBalancer getInstance()
    {
        if (instance == null)
        {
            instance = new LoadBalancer();
        }
        return instance;
    }

    /**
     * Returns a dictionary containing all available services.
     *
     * @return A dictionary containing all available services.
     */
    public Dictionary<Guid, string> GetAllServices()
    {
        return _services;
    }

    /**
     * Adds a new service to the dictionary of available services.
     * Generates a new GUID to use as the key for the new service.
     *
     * @param url The URL of the new service.
     * @return The GUID assigned to the new service.
     */
    public Guid AddService(string url)
    {
        var id = Guid.NewGuid();
        _services.Add(id, url);
        return id;
    }

    /**
     * Removes a service from the dictionary of available services.
     *
     * @param id The GUID of the service to remove.
     * @return The GUID of the service that was removed.
     */
    public Guid RemoveService(Guid id)
    {
        _services.Remove(id);
        return id;
    }

    /**
     * Returns the active load balancing strategy.
     *
     * @return The active load balancing strategy.
     */
    public ILoadBalancerStrategy GetActiveStrategy()
    {
        return _activeStrategy;
    }

    /**
     * Sets the active load balancing strategy based on the specified selection.
     * 1 - Round Robin strategy
     * 2 - Least Connected strategy
     *
     * @param selection The selection of the desired strategy.
     */
    public void SetActiveStrategy(int selection)
    {
        if (selection == 1)
        {
            UseRoundRobinStrategy();
        }
        if (selection == 2)
        {
            UseLeastConnectedStrategy();
        }
    }

    /**
     * Selects the next service to use for load balancing using the active
       strategy.
     * Increases the number of connections to the selected service in the services
       dictionary.
     * Returns the URL of the selected service.
     *
     * @return The URL of the selected service.
     */
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
        _leastConnectedStrategy = new LeastConnectedStrategy(_services);
        // Set the active strategy to the least-connected strategy.
        _activeStrategy = _leastConnectedStrategy;
    }
}