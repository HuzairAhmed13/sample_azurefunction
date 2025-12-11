using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace MyFunctionApp
{
    public class HelloAPI
    {
        private readonly ILogger _logger;

        public HelloAPI(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<HelloAPI>();
        }

        [Function("HelloAPI")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            // 1. Create your data object (The JSON content)
            var responseData = new
            {
                Message = "Hello from Azure! This is JSON data.",
                Timestamp = DateTime.UtcNow,
                Status = "Success"
            };

            // 2. Create the response and write JSON to it
            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(responseData);

            return response;
        }
    }
}
