using PortfolioEye.Domain.Entities;
using PortfolioEye.Infrastructure.Data;
using PortfolioEye.Infrastructure.Services;

namespace PortfolioEye.BackgroundWorkers;

public class PolishBondsInformationWorker(
    IServiceScopeFactory serviceScopeFactory,
    ILogger<PolishBondsInformationWorker> logger)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Delay(TimeSpan.FromSeconds(20), stoppingToken);
        await PerformExecution(stoppingToken);
        var sleepTimer = new PeriodicTimer(TimeSpan.FromDays(1));
        while (await sleepTimer.WaitForNextTickAsync(stoppingToken) || stoppingToken.IsCancellationRequested)
        {
            if (stoppingToken.IsCancellationRequested)
                return;
            await PerformExecution(stoppingToken);
        }
    }

    private async Task PerformExecution(CancellationToken stoppingToken)
    {
        try
        {
            await using var scope = serviceScopeFactory.CreateAsyncScope();
            await using var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = scope.ServiceProvider.GetRequiredService<BondInformationService>();
            var infoProvider = scope.ServiceProvider.GetRequiredService<BondInformationProvider>();
            var lastHistory = context.ImportHistory.Where(x => x.Type == ImportHistoryType.PolishRetailTreasuryBonds)
                .OrderByDescending(x => x.TimeStamp).FirstOrDefault();
            var currentBondVersion = infoProvider.GetCurrentBondVersion();
            if (lastHistory != null && Convert.ToInt32(lastHistory.Version) >= currentBondVersion)
                return;

            await service.ImportInformation();
            lastHistory = new ImportHistory
            {
                Version = currentBondVersion.ToString(),
                TimeStamp = DateTime.UtcNow,
                Type = ImportHistoryType.PolishRetailTreasuryBonds
            };
            await context.AddAsync(lastHistory, stoppingToken);
            await context.SaveChangesAsync(stoppingToken);
        }
        catch (Exception exc)
        {
            logger.LogError(exc, $"An error occurred during execution in {nameof(PolishBondsInformationWorker)}");
        }
    }
}