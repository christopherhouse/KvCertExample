using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace KvEnvVariable
{
    public class Function1
    {
        private readonly ILogger<Function1> _logger;

        public Function1(ILogger<Function1> logger)
        {
            _logger = logger;
        }

        [Function("Function1")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            var certData = Environment.GetEnvironmentVariable("cert");
            string response = null;

            try
            {
                var cert = new X509Certificate(Convert.FromBase64String(certData));
                response = "Cert acquired and instantiated successfully";
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to load certificate");
                response = "Failed to load certificate";
            }

            return new OkObjectResult(response);
        }
    }
}
