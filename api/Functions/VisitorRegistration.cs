using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using VR.Models;
using VR.Services;

namespace VR.Functions;

public class VisitorRegistration
{
    private readonly ILogger<VisitorRegistration> _logger;
    private readonly IDBService _dBService;

    public VisitorRegistration(ILogger<VisitorRegistration> logger, IDBService dBService)
    {
        _logger = logger;
        _dBService = dBService;
    }

    [Function("RegisterVisitor")]
    public async Task<IActionResult> RegisterVisitor([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
    {
        try
        {
            _logger.LogWarning("Testing Insight Logging.");

            var visitor = await req.ReadFromJsonAsync<RegisterVisitorDTO>();
            if (visitor == null)
            {
                throw new ArgumentNullException(nameof(req));
            }
            await _dBService.RegisterVisitor(visitor);

            _logger.LogInformation($"{nameof(visitor)}");

            return new OkObjectResult(new { visitor });
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogError(ex, "Trigger error while registering visitor from HttpRequest");
            return new BadRequestObjectResult("Invalid call to register visitor.");
        }
    }

    [Function("DisplayVisitors")]
    public async Task<IActionResult> DisplayVisitors([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)
    {
        try
        {
            var visitorList = await _dBService.GetAllVisitors();

            return new OkObjectResult(new { visitorList });
        }
        catch
        {
            return new BadRequestObjectResult("Error");
        }

    }
}