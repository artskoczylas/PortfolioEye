using MediatR;
using PortfolioEye.Common.Wrappers;

namespace PortfolioEye.Application.Features.Portfolios.Commands;

public class AddEditPortfolioCommand : IRequest<IResult>
{
    public Guid? Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}