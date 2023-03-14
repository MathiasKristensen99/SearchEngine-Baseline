using System;

namespace LoadBalancer.LoadBalancer;

public class LeastConnectedStrategy : ILoadBalancerStrategy
{
    public List<Service> _services= new List<Service>();
    public LeastConnectedStrategy(Dictionary<Guid, string> services)
    {
        foreach (var service in services)
        {
            _services.Add(new Service { Connections = 0, Url = service.Value});
        }
    }
    public string NextService(Dictionary<Guid, string> services)
    {
        int minConnections = int.MaxValue;
        List<Service> list = new List<Service>();

        foreach(var service in _services) {
            if(service.Connections < minConnections)
            {
                minConnections = service.Connections;
                list.Clear();
            }

            if (service.Connections == minConnections) {
                list.Add(service);
            }
        }

        var random = new Random();
        int index = random.Next(list.Count);
        var choosenService = list[index];

        foreach (var service in _services) {
            if (service.Url.Equals(choosenService.Url))
            {
                service.Connections++;
            }
        }

        Console.WriteLine("Used service:" + choosenService.Url + " Connections: " + choosenService.Connections);

        return choosenService.Url;
    }
}