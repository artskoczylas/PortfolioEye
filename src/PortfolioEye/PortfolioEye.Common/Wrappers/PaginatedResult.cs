namespace eastsoft.RCP.Shared.Wrappers
{
	public class PaginatedResult<T> : Result
	{
		public PaginatedResult(List<T> data, int? errorCode = null) : base(true, [], errorCode)
		{
			Data = data;
		}

		public List<T>? Data { get; set; }

		internal PaginatedResult(List<T>? data = default, int count = 0, int page = 1, int pageSize = 10)
			: base(true, [], null)
		{
			Data = data;
			CurrentPage = page;
			PageSize = pageSize;
			TotalPages = (int)Math.Ceiling(count / (double)pageSize);
			TotalCount = count;
		}

		internal PaginatedResult(int? errorCode, List<string>? messages = null)
			: base(false, messages ?? [], errorCode)
		{

		}

		public static PaginatedResult<T> Fail(int? errorCode, List<string> messages)
		{
			return new PaginatedResult<T>(errorCode, messages);
		}

		public static PaginatedResult<T> Success(List<T> data, int count, int page, int pageSize)
		{
			return new PaginatedResult<T>(data, count, page, pageSize);
		}

		public int CurrentPage { get; set; }

		public int TotalPages { get; set; }

		public int TotalCount { get; set; }
		public int PageSize { get; set; }

		public bool HasPreviousPage => CurrentPage > 1;

		public bool HasNextPage => CurrentPage < TotalPages;
	}
}
