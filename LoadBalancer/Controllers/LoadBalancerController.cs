using LoadBalancer.LoadBalancer;
using Microsoft.AspNetCore.Mvc;

namespace LoadBalancer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoadBalancerController : ControllerBase
{
    private readonly ILoadBalancer _loadBalancer;
    [HttpGet]
    public IActionResult Search(string terms, int numberOfResults)
    {
        // Hent addresse (url: string)
        var nextServiceUrl = _loadBalancer.NextService();
        
        // Brug Service URL
        using HttpClient client = new();
        client.BaseAddress = new Uri(nextServiceUrl);
        Task<string> task = client.GetStringAsync("api/search/" + terms);
        task.Wait();
        string searchResult = task.Result;
        return Ok(searchResult);
    }
}