using MediatR;
using PortfolioEye.Application.Features.Accounts.Common;
using PortfolioEye.Common.Wrappers;

namespace PortfolioEye.Application.Features.Accounts.Queries;

public record RetrieveAccountsByUserId(Guid UserId) : IRequest<IResult<RetrieveAccountsByUserId.Response>>
{
    public record Response(List<Portfolio> Portfolios);

    public record Portfolio(Guid Id, string Name, string Description, string Currency, AccountType Type, decimal Balance);
}