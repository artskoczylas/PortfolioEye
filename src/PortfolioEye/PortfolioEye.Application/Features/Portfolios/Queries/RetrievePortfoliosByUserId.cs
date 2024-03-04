using eastsoft.RCP.Shared.Wrappers;
using MediatR;
using static PortfolioEye.Application.Features.Portfolios.Queries.RetrievePortfoliosByUserId;

namespace PortfolioEye.Application.Features.Portfolios.Queries
{
	public record RetrievePortfoliosByUserId(Guid UserId) : IRequest<IResult<Response>>
	{
		public record Response(Guid Id, string Name);
	}
}
