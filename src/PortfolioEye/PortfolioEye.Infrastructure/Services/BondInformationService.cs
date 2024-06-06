using System.Data;
using Microsoft.EntityFrameworkCore;
using PortfolioEye.Domain.Entities;
using PortfolioEye.Infrastructure.Data;

namespace PortfolioEye.Infrastructure.Services;

public class BondInformationService(
    BondInformationProvider provider,
    BondInformationsReader reader,
    ApplicationDbContext context)
{
    public async Task ImportInformation()
    {
        var fileStream = provider.GetCurrentBondInformation();
        var newEmissions = reader.ReadInformation(fileStream).ToList();

        if (!newEmissions.Any())
            return;
        var existingEmissions = context.BondEmissions.Include(x => x.Years).ToList();

        foreach (var newEmission in newEmissions)
        {
            var existingEmission = existingEmissions.FirstOrDefault(x => x.Emission == newEmission.Series);
            if (existingEmission == null)
            {
                existingEmission = new BondEmission
                {
                    Kind = GetKind(newEmission.Kind),
                    Emission = newEmission.Series,
                    Isin = newEmission.Isin,
                    SaleStart = newEmission.SaleStart,
                    SaleEnd = newEmission.SaleEnd,
                    FirstYearInterestRate = newEmission.Years.First(x => x.YearNo == 1).InterestRate ??
                                            throw new DataException(
                                                $"Wrong first year interest rate for emission {newEmission.Series}"),
                    NextYearsInterestMargin = newEmission.Margin
                };
                existingEmission.Years = PrepareYears(existingEmission, existingEmission.Kind);
            }

            existingEmission.ConvertPrice = newEmission.ConvertPrice;
            foreach (var newYear in newEmission.Years)
            {
                var existingYear = existingEmission.Years.First(x => x.No == newYear.YearNo);
                existingYear.InterestRate = newYear.InterestRate;
            }

            context.BondEmissions.Update(existingEmission);
        }

        await context.SaveChangesAsync();
    }

    private BondEmissionKind GetKind(string kind)
    {
        return kind switch
        {
            "EDO" => BondEmissionKind.Edo,
            "ROD" => BondEmissionKind.Rod,
            _ => throw new NotSupportedException($"kind {kind} is not supported")
        };
    }

    private List<BondEmissionsYear> PrepareYears(BondEmission emission, BondEmissionKind kind)
    {
        return kind switch
        {
            BondEmissionKind.Edo => Enumerable.Range(1, 10)
                .Select(i => new BondEmissionsYear() { No = i, BondEmission = emission }).ToList(),
            BondEmissionKind.Rod => Enumerable.Range(1, 12)
                .Select(i => new BondEmissionsYear() { No = i, BondEmission = emission }).ToList(),
            _ => throw new NotSupportedException($"kind {kind} is not supported")
        };
    }
}