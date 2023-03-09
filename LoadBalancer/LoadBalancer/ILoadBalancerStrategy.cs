namespace LoadBalancer.LoadBalancer;

public interface ILoadBalancerStrategy
{
    public string NextService(Dictionary<Guid, string> services);
}