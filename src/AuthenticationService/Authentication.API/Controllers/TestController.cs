using Microsoft.AspNetCore.Mvc;

namespace Authentication.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    private readonly ILogger<TestController> _logger;
    private readonly IWebHostEnvironment _env;

    public TestController(ILogger<TestController> logger, IWebHostEnvironment env)
    {
        _logger = logger;
        _env = env;
    }

    [HttpGet]
    public IActionResult Get()
    {
        // Log the current directory and content root
        _logger.LogInformation("WebRootPath: {WebRootPath}", _env.WebRootPath);
        _logger.LogInformation("ContentRootPath: {ContentRootPath}", _env.ContentRootPath);
        _logger.LogInformation("Current Directory: {CurrentDirectory}", Directory.GetCurrentDirectory());
        
        _logger.LogInformation("This is an information log");
        _logger.LogWarning("This is a warning log");
        _logger.LogError("This is an error log");
        
        // Also write to console to verify logs are working
        Console.WriteLine("✅ Console.WriteLine - TestController called");
        
        return Ok(new { 
            message = "Check your logs!",
            webRoot = _env.WebRootPath,
            contentRoot = _env.ContentRootPath,
            currentDir = Directory.GetCurrentDirectory()
        });
    }
}