namespace PortfolioEye.Common.Wrappers
{
	public class Result(bool isSuccess, IEnumerable<string> messages, int? errorCode)
		: IResult
	{
		public bool IsSuccess { get; } = isSuccess;
		public IEnumerable<string> Messages { get; } = messages;
		public int? ErrorCode { get; } = errorCode;

		public static IResult Fail(int errorCode) => new Result(false, [], errorCode);
		public static IResult Fail(int errorCode, string message) => new Result(false, [message], errorCode);
		public static IResult Fail(int errorCode, List<string> messages) => new Result(false, messages, errorCode);

		public static Task<IResult> FailAsync(int errorCode) => Task.FromResult(Fail(errorCode));
		public static Task<IResult> FailAsync(int errorCode, string message) => Task.FromResult(Fail(errorCode, message));
		public static Task<IResult> FailAsync(int errorCode, List<string> messages) => Task.FromResult(Fail(errorCode, messages));

		public static IResult Success() => new Result(true, [], null);
		public static IResult Success(string message) => new Result(true, [message], null);

		public static Task<IResult> SuccessAsync() => Task.FromResult(Success());
		public static Task<IResult> SuccessAsync(string message) => Task.FromResult(Success(message));
	}

	public class Result<T> : Result, IResult<T>
	{
		public T? Data { get; }

		public Result(bool isSuccess, IEnumerable<string> messages, int? errorCode, T? data)
			:base(isSuccess, messages, errorCode)
		{
			if (isSuccess)
				ArgumentNullException.ThrowIfNull(nameof(data));
			Data = data;
		}

		public new static IResult<T> Fail(int errorCode) => new Result<T>(false, [], errorCode, default);
		public new static IResult<T> Fail(int errorCode, string message) => new Result<T>(false, [message], errorCode, default);
		public new static IResult<T> Fail(int errorCode, List<string> messages) => new Result<T>(false, messages, errorCode, default);

		public new static Task<IResult<T>> FailAsync(int errorCode) => Task.FromResult(Fail(errorCode));
		public new static Task<IResult<T>> FailAsync(int errorCode, string message) => Task.FromResult(Fail(errorCode, message));
		public new static Task<IResult<T>> FailAsync(int errorCode, List<string> messages) => Task.FromResult(Fail(errorCode, messages));

		public new static IResult<T> Success() => new Result<T>(true, [], null, default);
		public new static IResult<T> Success(string message) => new Result<T>(true, [message], null, default);

		public static IResult<T> Success(T data) => new Result<T>(true, [], null, data);
		public static IResult<T> Success(T data, string message) => new Result<T>(true, [message], null, data);
		public static IResult<T> Success(T data, List<string> messages) => new Result<T>(true, messages, null, data);

		public new static Task<IResult<T>> SuccessAsync() => Task.FromResult(Success());

		public new static Task<IResult<T>> SuccessAsync(string message) => Task.FromResult(Success(message));

		public static Task<IResult<T>> SuccessAsync(T data) => Task.FromResult(Success(data));
		public static Task<IResult<T>> SuccessAsync(T data, string message) => Task.FromResult(Success(data, message));
	}
}
