﻿using MediatR;
using PortfolioEye.Common.Wrappers;

namespace PortfolioEye.Application.Features.Portfolios.Queries
{
	public record RetrievePortfoliosByUserId(Guid UserId) : IRequest<IResult<RetrievePortfoliosByUserId.Response>>
	{
		public record Response(List<Portfolio> Portfolios);

		public record Portfolio(Guid Id, string Name, string Description, string Currency, decimal Balance);
	}
}
