using PortfolioEye.Common.Wrappers;

namespace PortfolioEye.Common.Extensions;

public static class ResultExt
{
    public static Task<IResult<T>> ToSuccessResultAsync<T>(this T source)
    {
        return Result<T>.SuccessAsync(source);
    }
}