using PortfolioEye.Infrastructure.Data;

namespace PortfolioEye.BackgroundWorkers;

public class PolishBondsInformationWorker(
    IServiceScopeFactory serviceScopeFactory,
    ILogger<PolishBondsInformationWorker> logger)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await PerformExecution(stoppingToken);
        var sleepTimer = new PeriodicTimer(TimeSpan.FromDays(1));
        while (await sleepTimer.WaitForNextTickAsync(stoppingToken) || stoppingToken.IsCancellationRequested)
        {
            if(stoppingToken.IsCancellationRequested)
                return;
            await PerformExecution(stoppingToken);
        }
    }

    private async Task PerformExecution(CancellationToken stoppingToken)
    {
        try
        {
            await using var scope = serviceScopeFactory.CreateAsyncScope();
            using var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        }
        catch (Exception exc)
        {
            logger.LogError(exc, $"An error occurred during execution in {nameof(PolishBondsInformationWorker)}");
        }
    }
}