using MediatR;
using PortfolioEye.Common.Wrappers;

namespace PortfolioEye.Application.Features.Currencies.Commands;

public record AddCurrencyCommand(string Code, bool Enabled) : IRequest<IResult>;