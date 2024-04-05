using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using PortfolioEye.Application.Features.Portfolios.Queries;

namespace PortfolioEye.Client.Pages
{
    public partial class Portfolios
    {
        private string _searchString = "";
        private bool _loaded = false;
        private List<RetrievePortfoliosByUserId.Response>? _portfolios;
        private RetrievePortfoliosByUserId.Response? _portfolio;
        [Inject] public IStringLocalizer<Portfolios>? Localizer { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await Task.Delay(400);
            _portfolios =
            [
                new RetrievePortfoliosByUserId.Response(Guid.NewGuid(), "Test1"),
                new RetrievePortfoliosByUserId.Response(Guid.NewGuid(), "Test2"),
                new RetrievePortfoliosByUserId.Response(Guid.NewGuid(), "Test3")
            ];
            _loaded = true;
        }

        private bool Search(RetrievePortfoliosByUserId.Response portfolio)
        {
            if (string.IsNullOrWhiteSpace(_searchString)) return true;
            return portfolio.Name?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true;
        }

        private async Task CreateNew()
        {
            await Task.Delay(10);
        }
        private async Task Edit(RetrievePortfoliosByUserId.Response portfolio)
        {
            await Task.Delay(10);
        }
        private async Task Delete(RetrievePortfoliosByUserId.Response portfolio)
        {
            await Task.Delay(10);
        }
        
    }
}