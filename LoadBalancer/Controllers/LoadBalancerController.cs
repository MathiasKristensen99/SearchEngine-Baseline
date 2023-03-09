using Microsoft.AspNetCore.Mvc;

namespace LoadBalancer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoadBalancerController : ControllerBase
{
    [HttpGet]
    public IActionResult Search()
    {
        //TODO - Implementation Here
        return null;
    }
}