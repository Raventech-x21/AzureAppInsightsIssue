using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Log : ControllerBase
    {
        private readonly ILogger<Log> _logger;

        public Log(ILogger<Log> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string correlationId)
        {
            _logger.LogInformation($"[{correlationId}] Logging information message in Web Api");

            return Ok();
        }
    }
}
