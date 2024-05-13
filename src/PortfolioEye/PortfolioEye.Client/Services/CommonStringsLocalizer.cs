using Microsoft.Extensions.Localization;

namespace PortfolioEye.Client.Services;

public class CommonStringsLocalizer<T> : IStringLocalizer<T>
{
    private readonly IStringLocalizer _classLocalizer;
    private readonly IStringLocalizer _commonLocalizer;

    public CommonStringsLocalizer(IStringLocalizerFactory factory)
    {
        this._classLocalizer = factory.Create(typeof(T));
        this._commonLocalizer = factory.Create("Common", typeof(CommonStringsLocalizer<>).Assembly.FullName);
    }

    public LocalizedString this[string name]
    {
        get
        {
            var result = _classLocalizer[name];
            return result.ResourceNotFound ? _commonLocalizer[name] : result;
        }
    }

    public LocalizedString this[string name, params object[] arguments]
    {
        get
        {
            var result = _classLocalizer[name, arguments];
            return result.ResourceNotFound ? _commonLocalizer[name, arguments] : result;
        }
    }
		
    public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
    {
        var common = _commonLocalizer.GetAllStrings(includeParentCultures);
        var local = _classLocalizer.GetAllStrings(includeParentCultures);
        var result = local.Union(common.Where(x => !local.Any(y => y.Name == x.Name))).ToList();
        return common.Where(x => !local.Any(y => y.Name == x.Name)).Union(local).ToList();
    }
}