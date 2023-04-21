namespace DynamicalLogLevel.Api;

public class Worker:BackgroundService
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
            _logger?.LogTrace("This is a trace message...");
            _logger?.LogDebug("This is a debug message...");
            _logger?.LogInformation("This is an information message...");
            _logger?.LogWarning("This is a warning message...");
            _logger?.LogError("This is an error message...");
            _logger?.LogCritical("This is a critical message...");

            await Task.Delay(1000);
        }
    }
}