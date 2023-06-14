namespace LoggingWorker
{
    public static partial class Log
    {
        [LoggerMessage(
            EventId = 12345,
            Level = LogLevel.Critical,
            Message = "Worker running at: {now} (code gen)")]
        public static partial void LogRunningMessage(
            ILogger logger, DateTimeOffset now);
    }

    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);

                Log.LogRunningMessage(_logger, DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}