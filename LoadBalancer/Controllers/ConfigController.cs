using LoadBalancer.LoadBalancer;
using Microsoft.AspNetCore.Mvc;

namespace LoadBalancer.Controllers;

[Route("api/[controller]")]
[ApiController]

public class ConfigController : ControllerBase
{
    private readonly ILoadBalancer _loadBalancer = LoadBalancer.LoadBalancer.getInstance();
    
    [HttpPost]
    public void RegisterService(string url)
    {
        _loadBalancer.AddService(url);
    }
}