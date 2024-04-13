using MediatR;
using PortfolioEye.Common.Wrappers;

namespace PortfolioEye.Application.Features.Accounts.Commands;

public record DeleteAccountByIdCommand(Guid Id) : IRequest<IResult>;