using MediatR;
using PortfolioEye.Common.Wrappers;

namespace PortfolioEye.Application.Features.Portfolios.Commands;

public record DeletePortfolioByIdCommand(Guid Id) : IRequest<IResult>;