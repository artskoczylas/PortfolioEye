using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioEye.Application;
using PortfolioEye.Application.Features.Currencies.Commands;
using PortfolioEye.Common.Wrappers;
using PortfolioEye.Domain.Entities;
using PortfolioEye.Infrastructure.Data;

namespace PortfolioEye.Infrastructure.Handlers.Currencies.Commands;

public class AddCurrencyCommandHandler(ApplicationDbContext context) : IRequestHandler<AddCurrencyCommand, IResult>
{
    public async Task<IResult> Handle(AddCurrencyCommand request, CancellationToken cancellationToken)
    {
        var currencyExists = await context.Currencies.AnyAsync(c => c.Code == request.Code, cancellationToken);
        if (currencyExists)
            return await Result.FailAsync(WellKnown.ErrorCodes.Conflict,
                $"Currency with code '{request.Code}' already exists.");

        var currency = request.Adapt<Currency>();

        await context.Currencies.AddAsync(currency);
        await context.SaveChangesAsync(cancellationToken);

        return await Result.SuccessAsync($"Currency with code '{request.Code}' has been added successfully.");
    }
}