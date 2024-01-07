using Azure.Identity;
using Azure.Monitor.Query;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace TimerFunctionApp
{
    public class Function1
    {
        private readonly ILogger _logger;
        private const int NumberOfMessages = 1000;

        public Function1(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Function1>();
        }

        [Function("Function1")]
        public async Task Run([TimerTrigger("0 0 * * * *")] TimerInfo myTimer)
        {
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            if (myTimer.ScheduleStatus is not null)
            {
                _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
            }

            var correlationId = Guid.NewGuid();

            for (int i = 1; i <= NumberOfMessages; i++)
            {
                _logger.LogInformation($"[{correlationId}] Logging information message #{i}");
                _logger.LogWarning($"[{correlationId}] Logging warning message #{i}");
                _logger.LogError($"[{correlationId}] Logging error message #{i}");
            }

            _logger.LogInformation($"Waiting ten minutes before checking logs...");

            await Task.Delay(TimeSpan.FromMinutes(10));

            _logger.LogInformation($"Checking logs...");

            var workspaceId = "363c3473-7f57-4605-be76-f2b58605c2ca";
            var credential = new DefaultAzureCredential();
            var client = new LogsQueryClient(credential);
            var timeRange = new QueryTimeRange(TimeSpan.FromHours(1));

            var kqlQuery = $"AppTraces | where OperationName == 'Function1' and Message startswith '[{correlationId}]' and SeverityLevel == 1 | count";

            var response = await client.QueryWorkspaceAsync(workspaceId, kqlQuery, timeRange);

            var messageCount = int.Parse(response.Value.Table.Rows[0][0].ToString());

            _logger.LogInformation($"Count of messages: {messageCount}");

            // TODO : change this back to != NumberOfMessages when i've tested the alert
            if (messageCount != 500)
                _logger.LogCritical($"Did not log {NumberOfMessages} messages");
        }
    }
}
