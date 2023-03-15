using System;

namespace LoadBalancer.LoadBalancer;

public class LeastConnectedStrategy : ILoadBalancerStrategy
{
    public List<Service> _services = new List<Service>();
    public LeastConnectedStrategy(Dictionary<Guid, string> services)
    {
        foreach (var service in services)
        {
            _services.Add(new Service { Connections = 0, Url = service.Value});
        }
    }
    public string NextService(Dictionary<Guid, string> services)
    {
        // Initialize variables
        int minConnections = int.MaxValue;
        List<Service> list = new List<Service>();
        
        // Find the services with the minimum number of connections
        foreach(var service in _services) {
            if(service.Connections < minConnections)
            {
                // If a new minimum is found, clear the list of services with the previous minimum
                minConnections = service.Connections;
                list.Clear();
            }
            
            // Add the current service to the list of services with the current minimum
            if (service.Connections == minConnections) {
                list.Add(service);
            }
        }
        
        // Choose a random service from the list of services with the current minimum
        var random = new Random();
        int index = random.Next(list.Count);
        var chosenService = list[index];

        // Increment the number of connections for the chosen service
        foreach (var service in _services) {
            if (service.Url.Equals(chosenService.Url))
            {
                service.Connections++;
            }
        }
        
        // Output the service that was used and the number of connections it now has
        Console.WriteLine("Used service:" + chosenService.Url + " Connections: " + chosenService.Connections);

        // Return the URL of the chosen service
        return chosenService.Url;
    }
}