using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;
using PortfolioEye.Application.Features.Portfolios.Commands;
using PortfolioEye.Application.Features.Portfolios.Queries;
using PortfolioEye.Client.Components.Dialogs;
using PortfolioEye.Client.Infrastructure.Managers;

namespace PortfolioEye.Client.Pages
{
    public partial class Portfolios
    {
        private string _searchString = "";
        private bool _loaded = false;
        private IEnumerable<RetrievePortfoliosByUserId.Response>? _portfolios;
        private RetrievePortfoliosByUserId.Response? _portfolio;
        [Inject] public IStringLocalizer<Portfolios> Localizer { get; set; } = null!;
        [Inject] public IDialogService DialogService { get; set; } = null!;
        [Inject] public IPortfoliosManager PortfoliosManager { get; set; } = null!;
        [Inject] public ISnackbar Snackbar { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            var result = await PortfoliosManager.RetrieveAllMy();
            if (result.IsSuccess)
                _portfolios = result.Data;
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
            await ShowDialog();
        }

        private async Task Edit(RetrievePortfoliosByUserId.Response portfolio)
        {
            await ShowDialog(portfolio.Id);
        }

        private async Task Delete(RetrievePortfoliosByUserId.Response portfolio)
        {
            var dialogResult = await DialogService.ShowMessageBox(Localizer["TitleDeletePortfolio"]
                , Localizer["ContentDeletePortfolio"]
                , Localizer["Yes"]
                , Localizer["No"]);
            if (!dialogResult.HasValue || !dialogResult.Value)
                return;
            var deleteResult = await PortfoliosManager!.Delete(portfolio.Id);
            if (deleteResult.IsSuccess)
                Snackbar.Add(Localizer["PortfolioDeleted"], Severity.Success);
            else
                Snackbar.Add(Localizer["CannotDeletePortfolio"], Severity.Warning);
        }

        private async Task ShowDialog(Guid? id = null)
        {
            var parameters = new DialogParameters();
            if (id != null && _portfolios?.FirstOrDefault(c => c.Id == id) != null)
            {
                parameters.Add(nameof(AddEditPortfolioDialog.PortfolioId), id);
            }

            var options = new DialogOptions
                { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog =
                await DialogService.ShowAsync<AddEditPortfolioDialog>(
                    id == null ? Localizer!["Create"] : Localizer!["Edit"],
                    parameters, options);
            var result = await dialog.Result;
            if (!result.Canceled)
            {
                await Refresh();
            }
        }
    }
}