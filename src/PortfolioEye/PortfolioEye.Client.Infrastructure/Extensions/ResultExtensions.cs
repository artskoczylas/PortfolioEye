using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using eastsoft.RCP.Shared.Wrappers;

namespace PortfolioEye.Client.Infrastructure.Extensions
{
	public static class ResultExtensions
	{
		private static readonly JsonSerializerOptions defaultOptions = new()
		{
			PropertyNameCaseInsensitive = true,
			ReferenceHandler = ReferenceHandler.Preserve
		};
		public static async Task<IResult<T>> ToResult<T>(this HttpResponseMessage response)
		{
			var responseAsString = await response.Content.ReadAsStringAsync();
			var responseObject = JsonSerializer.Deserialize<Result<T>>(responseAsString, defaultOptions);
			return responseObject ?? Result<T>.Fail((int)response.StatusCode);
		}

		public static async Task<IResult> ToResult(this HttpResponseMessage response)
		{
			var responseAsString = await response.Content.ReadAsStringAsync();
			var responseObject = JsonSerializer.Deserialize<Result>(responseAsString, defaultOptions);
			return responseObject ?? Result.Fail((int)response.StatusCode);
		}

		public static async Task<PaginatedResult<T>> ToPaginatedResult<T>(this HttpResponseMessage response)
		{
			var responseAsString = await response.Content.ReadAsStringAsync();
			var responseObject = JsonSerializer.Deserialize<PaginatedResult<T>>(responseAsString, defaultOptions);
			return responseObject ?? PaginatedResult<T>.Fail((int)response.StatusCode, []);
		}
	}
}
