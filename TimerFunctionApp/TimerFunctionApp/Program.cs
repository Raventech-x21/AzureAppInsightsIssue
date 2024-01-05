using Microsoft.Extensions.Hosting;

// .NET 7.0 template implementation
var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services => { })
    .Build();

// .NET 8.0 template implementation
//var host = new HostBuilder()
//    .ConfigureFunctionsWorkerDefaults()
//    .ConfigureServices(services =>
//    {
//        services.AddApplicationInsightsTelemetryWorkerService();
//        services.ConfigureFunctionsApplicationInsights();
//    })
//    .ConfigureLogging(logging =>
//    {
//        // https://learn.microsoft.com/en-au/azure/azure-functions/dotnet-isolated-process-guide#application-insights
//        logging.Services.Configure<LoggerFilterOptions>(options =>
//        {
//            var defaultRule = options.Rules.FirstOrDefault(rule => rule.ProviderName == "Microsoft.Extensions.Logging.ApplicationInsights.ApplicationInsightsLoggerProvider");

//            if (defaultRule != null)
//                options.Rules.Remove(defaultRule);
//        });
//    })
//    .Build();

host.Run();
