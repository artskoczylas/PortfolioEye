using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Text;
using Microsoft.Extensions.Localization;

namespace PortfolioEye.Client.Services
{
	public class BreadcrumbService
	{
		private readonly IStringLocalizer<BreadcrumbService> _localizer;
		private readonly NavigationManager _navigationManager;
		private string _currentUrl;
		public BreadcrumbService(NavigationManager navigationManager, IStringLocalizer<BreadcrumbService> localizer)
		{
			this._navigationManager = navigationManager;
			_localizer = localizer;
			navigationManager.LocationChanged += NavigationManager_LocationChanged;
			_currentUrl = navigationManager.ToBaseRelativePath(navigationManager.Uri);
		}

		private void NavigationManager_LocationChanged(object? sender, Microsoft.AspNetCore.Components.Routing.LocationChangedEventArgs e)
		{
			_currentUrl = _navigationManager.ToBaseRelativePath(e.Location);
		}

		public List<BreadcrumbItem> GetBreadcrumbs()
		{
			var result = new List<BreadcrumbItem>() { new BreadcrumbItem(_localizer["Home"], href: "#") };

			if (string.IsNullOrEmpty(_currentUrl))
				return result;

			var url = new StringBuilder();
			var splitted = _currentUrl.Split('/');
			foreach (var item in splitted)
			{
				url.Append("/").Append(item);
				result.Add(new BreadcrumbItem(_localizer[item.ToLower()], href: url.ToString()));
			}

			return result;
		}
	}
}
