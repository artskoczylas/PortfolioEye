using System.Text.Json.Serialization;
using System.Text.Json;
using PortfolioEye.Common.Wrappers;

namespace PortfolioEye.Client.Infrastructure.Extensions
{
	public static class ResultExtensions
	{
		private static readonly JsonSerializerOptions DefaultOptions = new()
		{
			PropertyNameCaseInsensitive = true,
			ReferenceHandler = ReferenceHandler.Preserve
		};
		public static async Task<IResult<T>> ToResultAsync<T>(this HttpResponseMessage response)
		{
			var responseAsString = await response.Content.ReadAsStringAsync();
			var responseObject = JsonSerializer.Deserialize<Result<T>>(responseAsString, DefaultOptions);
			return responseObject ?? Result<T>.Fail((int)response.StatusCode);
		}

		public static async Task<IResult> ToResultAsync(this HttpResponseMessage response)
		{
			var responseAsString = await response.Content.ReadAsStringAsync();
			var responseObject = JsonSerializer.Deserialize<Result>(responseAsString, DefaultOptions);
			return responseObject ?? Result.Fail((int)response.StatusCode);
		}

		public static async Task<PaginatedResult<T>> ToPaginatedResult<T>(this HttpResponseMessage response)
		{
			var responseAsString = await response.Content.ReadAsStringAsync();
			var responseObject = JsonSerializer.Deserialize<PaginatedResult<T>>(responseAsString, DefaultOptions);
			return responseObject ?? PaginatedResult<T>.Fail((int)response.StatusCode, []);
		}
	}
}
