using LoadBalancer.LoadBalancer;
using Microsoft.AspNetCore.Mvc;

namespace LoadBalancer.Controllers;

[Route("api/[controller]")]
[ApiController]

public class ConfigurationController : ControllerBase
{
    private readonly ILoadBalancer _loadBalancer = LoadBalancer.LoadBalancer.getInstance();

    [HttpPost]
    public Guid AddService([FromQuery] string url)
    {
        Console.WriteLine("Adding service at URL " + url);
        return LoadBalancer.LoadBalancer.getInstance().AddService(url);
    }

    [HttpPost("SetStrategy")]
    public void SetStrategy([FromQuery] int selection)
    {
        Console.WriteLine("Strategy changed to " + selection);
        LoadBalancer.LoadBalancer.getInstance().SetActiveStrategy(selection);
    }
}