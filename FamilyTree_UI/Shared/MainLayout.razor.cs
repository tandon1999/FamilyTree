using Blazored.Toast.Services;
using FamilyTree_UI.Shared.Component;
using FamilyTree_UI.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace FamilyTree_UI.Shared
{
    public partial class MainLayout
    {

        private Loader loader;
        [Inject] private LoaderService LoaderService { get; set; }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            try
            {
                if (firstRender)
                {
                    if (loader != null)
                    {
                        LoaderService.RegisterShowAction(loader.ShowLoader);
                        LoaderService.RegisterHideAction(loader.HideLoader);
                        StateHasChanged();
                    }
                }
            }
            catch (Exception ex)
            {
                _toastservice.ShowError(ex.Message);
            }
        }
    }
}