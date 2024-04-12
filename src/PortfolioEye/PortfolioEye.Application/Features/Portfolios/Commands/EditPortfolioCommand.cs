using MediatR;
using PortfolioEye.Common.Wrappers;

namespace PortfolioEye.Application.Features.Portfolios.Commands;

public record AddPortfolioCommand(string Name, string Description, string Currency)
    : IRequest<IResult>;

public record EditPortfolioCommand(Guid Id, string Name, string Description)
    : IRequest<IResult>;

public record AddPortfolioForUserCommand(Guid UserId, AddPortfolioCommand BaseCommand)
    : AddPortfolioCommand(BaseCommand);

public record EditPortfolioForUserCommand(Guid UserId, EditPortfolioCommand BaseEditCommand)
    : EditPortfolioCommand(BaseEditCommand);