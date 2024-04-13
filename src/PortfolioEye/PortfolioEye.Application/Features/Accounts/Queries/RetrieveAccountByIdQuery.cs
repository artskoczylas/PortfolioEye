using MediatR;
using PortfolioEye.Application.Features.Accounts.Common;
using PortfolioEye.Common.Wrappers;

namespace PortfolioEye.Application.Features.Accounts.Queries;

public record RetrieveAccountByIdQuery(Guid Id) : IRequest<IResult<RetrieveAccountByIdQuery.Response>>
{
    public record Response(Guid Id, string Name, string Description,  string Currency, AccountType Type ,decimal Balance);
}