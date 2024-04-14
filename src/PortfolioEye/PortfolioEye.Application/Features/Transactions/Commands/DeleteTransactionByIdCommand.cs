using MediatR;
using PortfolioEye.Common.Wrappers;

namespace PortfolioEye.Application.Features.Transactions.Commands;

public record DeleteTransactionByIdCommand(Guid Id) : IRequest<IResult>;