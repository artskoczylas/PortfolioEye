using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;
using PortfolioEye.Application.Features.Portfolios.Commands;
using PortfolioEye.Application.Features.Portfolios.Queries;
using PortfolioEye.Client.Components.Dialogs;

namespace PortfolioEye.Client.Pages
{
    public partial class Portfolios
    {
        private string _searchString = "";
        private bool _loaded = false;
        private List<RetrievePortfoliosByUserId.Response>? _portfolios;
        private RetrievePortfoliosByUserId.Response? _portfolio;
        [Inject] public IStringLocalizer<Portfolios>? Localizer { get; set; }
        [Inject] public IDialogService DialogService { get; set; }
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

        public async Task Refresh()
        {
            await Task.Delay(10);
        }
        
        private bool Search(RetrievePortfoliosByUserId.Response portfolio)
        {
            if (string.IsNullOrWhiteSpace(_searchString)) return true;
            return portfolio.Name?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true;
        }

        private async Task CreateNew()
        {
            await Task.Delay(10);
            await ShowDialog();
        }
        private async Task Edit(RetrievePortfoliosByUserId.Response portfolio)
        {
            await Task.Delay(10);
        }
        private async Task Delete(RetrievePortfoliosByUserId.Response portfolio)
        {
            await Task.Delay(10);
        }
        
        private async Task ShowDialog(Guid? id = null)
        {
            var parameters = new DialogParameters();
            if (id != null)
            {
                _portfolio = _portfolios?.FirstOrDefault(c => c.Id == id);
                if (_portfolio != null)
                {
                    parameters.Add(nameof(AddEditPortfolioDialog.AddEditBrandModel), new AddEditPortfolioCommand()
                    {
                        Id = _portfolio.Id,
                        Name = _portfolio.Name,
                        //Description = _portfolio.Description,
                    });
                }
            }
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = DialogService.Show<AddEditPortfolioDialog>(id == null ? Localizer!["Create"] : Localizer!["Edit"], parameters, options);
            var result = await dialog.Result;
            if (!result.Canceled)
            {
                await Refresh();
            }
        }
    }
}