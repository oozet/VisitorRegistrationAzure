using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace api
{
    public class TestEndpoint
    {
        private readonly ILogger _logger;

        public TestEndpoint(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<TestEndpoint>();
        }

        [Function("TestEndpoint")]
        public HttpResponseData Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            _logger.LogInformation("TestEndpoint function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "application/json; charset=utf-8");
            response.WriteString("{\"message\": \"Hello from .NET 9 Azure Function!\"}");

            return response;
        }
    }
}
