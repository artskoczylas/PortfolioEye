namespace eastsoft.RCP.Shared.Wrappers
{
	public interface IResult
	{
		IEnumerable<string> Messages { get; }
		bool IsSuccess { get; }
		int? ErrorCode { get; }
	}
	public interface IResult<out T> : IResult
	{
		T? Data { get; }
	}
}
