namespace LoadBalancer.LoadBalancer;

public class LoadBalancer : ILoadBalancer
{
    private static ILoadBalancer instance;
    private LoadBalancer() { }
    private Dictionary<Guid, string> services = new ();

    public static ILoadBalancer getInstance()
    {
        if (instance == null)
        {
            instance = new LoadBalancer();
        }
        return instance;
    }
    public List<string> GetAllServices()
    {
        throw new NotImplementedException();
    }

    public Guid AddService(string url)
    {
        var id = Guid.NewGuid();
        services.Add(id,url);
        return id;

    }

    public Guid RemoveService(Guid id)
    {
        services.Remove(id);
        return id;
    }

    public ILoadBalancerStrategy GetActiveStrategy()
    {
        throw new NotImplementedException();
    }

    public void SetActiveStrategy(ILoadBalancerStrategy strategy)
    {
        throw new NotImplementedException();
    }

    public string NextService()
    {
        throw new NotImplementedException();
    }
}