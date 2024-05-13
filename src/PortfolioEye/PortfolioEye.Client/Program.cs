using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Localization;
using MudBlazor.Services;
using PortfolioEye.Client;
using PortfolioEye.Client.Infrastructure;
using PortfolioEye.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddMudServices();

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();
builder.Services.AddTransient<BreadcrumbService>();
builder.Services.AddInfrastructureLayer();
builder.Services.AddHttpClient("MainApi", c => c.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
builder.Services.AddLocalization(options =>
{
    options.ResourcesPath = "Resources";
});
builder.Services.AddTransient(typeof(IStringLocalizer<>), typeof(CommonStringsLocalizer<>));
await builder.Build().RunAsync();
