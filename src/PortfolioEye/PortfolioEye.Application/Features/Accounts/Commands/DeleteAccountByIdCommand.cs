using MediatR;
using PortfolioEye.Common.Wrappers;

namespace PortfolioEye.Application.Features.Accounts.Commands;

public record DeletePortfolioByIdCommand(Guid Id) : IRequest<IResult>;