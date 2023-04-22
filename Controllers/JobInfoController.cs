using Microsoft.AspNetCore.Mvc;

namespace JobHunt.Controller;

[ApiController]
public class JobInfoController : ControllerBase
{
    [HttpGet("/jobinfo")]
    public string Get()
    {
        return "Hello";
    }
}