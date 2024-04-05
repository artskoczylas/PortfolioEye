using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.Extensions.Localization;
using MudBlazor;
using PortfolioEye.Application.Features.Users.Queries;
using PortfolioEye.Client.Infrastructure.Managers;
using PortfolioEye.Client.Services;

namespace PortfolioEye.Client.Layout;

public partial class MainLayout
{
    [Inject] public IStringLocalizer<MainLayout>? Localizer { get; set; }
    private bool _drawerOpen = true;

    private void DrawerToggle()
    {
        _drawerOpen = !_drawerOpen;
    }

    private UserProfileResponse? _profile;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            var result = await CurrentUserManager!.RetrieveMyProfile();
            if (result.IsSuccess)
                _profile = result.Data;
        }
        catch
        {
            //Error when not logged in
        }

        NavigationManager!.LocationChanged += OnLocationChanged;
        _items = BreadcrumbService!.GetBreadcrumbs();
        await base.OnInitializedAsync();
    }

    private void OnLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        // currentUrl = NavigationManager.ToBaseRelativePath(e.Location);
        _items = BreadcrumbService!.GetBreadcrumbs();
        StateHasChanged();
    }

    private List<BreadcrumbItem> _items = [new BreadcrumbItem("Home", href: "#")];
    private readonly MudTheme _currentTheme = new PortfolioEyeTheme();
    [Inject] protected NavigationManager? NavigationManager { get; set; }
    [Inject] protected BreadcrumbService? BreadcrumbService { get; set; }
    [Inject] public ICurrentUserManager? CurrentUserManager { get; set; }
    private void NavigateWithReload(string url) => NavigationManager?.NavigateTo(url, true);
}