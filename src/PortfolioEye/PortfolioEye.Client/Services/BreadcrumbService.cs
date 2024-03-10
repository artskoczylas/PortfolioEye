using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Text;

namespace PortfolioEye.Client.Services
{
	public class BreadcrumbService
	{
		private readonly NavigationManager navigationManager;
		private string currentUrl;
		public BreadcrumbService(NavigationManager navigationManager)
		{
			this.navigationManager = navigationManager;
			navigationManager.LocationChanged += NavigationManager_LocationChanged;
			currentUrl = navigationManager.ToBaseRelativePath(navigationManager.Uri);
		}

		private void NavigationManager_LocationChanged(object? sender, Microsoft.AspNetCore.Components.Routing.LocationChangedEventArgs e)
		{
			currentUrl = navigationManager.ToBaseRelativePath(e.Location);
		}

		public List<BreadcrumbItem> GetBreadcrumbs()
		{
			var result = new List<BreadcrumbItem>() { new BreadcrumbItem("Home", href: "#") };

			if (string.IsNullOrEmpty(currentUrl))
				return result;

			var url = new StringBuilder();
			var splitted = currentUrl.Split('/');
			foreach (var item in splitted)
			{
				url.Append("/").Append(item);
				result.Add(new BreadcrumbItem(item, href: url.ToString()));
			}

			return result;
		}
	}
}
