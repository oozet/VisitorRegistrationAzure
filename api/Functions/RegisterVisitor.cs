using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using VR.Models;

namespace VR.Functions;

public class RegisterVisitor
{
    private readonly ILogger<RegisterVisitor> _logger;

    public RegisterVisitor(ILogger<RegisterVisitor> logger)
    {
        _logger = logger;
    }

    [Function("RegisterVisitor")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req, [FromBody] Visitor visitor)
    {
        _logger.LogWarning("Testing Insight Logging.");
        return new OkObjectResult(new { visitor });
    }
}