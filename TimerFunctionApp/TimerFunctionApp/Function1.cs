using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace TimerFunctionApp
{
    public class Function1
    {
        private readonly ILogger _logger;

        public Function1(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Function1>();
        }

        [Function("Function1")]
        public void Run([TimerTrigger("0 0 * * * *")] TimerInfo myTimer)
        {
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            if (myTimer.ScheduleStatus is not null)
            {
                _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
            }

            var correlationId = Guid.NewGuid();

            for (int i = 1; i <= 100; i++)
            {
                _logger.LogInformation($"[{correlationId}] Logging information message #{i}");
                _logger.LogWarning($"[{correlationId}] Logging warning message #{i}");
                _logger.LogError($"[{correlationId}] Logging error message #{i}");
            }
        }
    }
}
