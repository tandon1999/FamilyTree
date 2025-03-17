using Blazored.Toast.Services;
using FamilyTree_UI.Manager.Interface.Auth;
using FamilyTree_UI.Models.AuthModel;
using FamilyTree_UI.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace FamilyTree_UI.Shared
{
    public partial class NavMenu
    {
        private bool collapseNavMenu = true;
        private string? NavMenuCssClass => collapseNavMenu ? "collapse" : null;
        [Inject] public NavStateService NavStateService { get; set; }
        [Inject] public IToastService _toastservice { get; set; } = default!;
        [Inject] public NavigationManager _navigationManager { get; set; } = default!;
        private DateTime? _loginExpiryTime;
        private readonly TimeSpan _sessionDuration = TimeSpan.FromMinutes(1);
        [Parameter] public DateTime? LoginExpiry { get; set; }
        [Inject] private LoaderService _loader { get; set; } = default!;

        private void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }
        protected override void OnInitialized()
        {
            _loader.ShowLoader();
            try
            {
                NavStateService.OnNavStateChanged += StateHasChanged;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                _loader.HideLoader();
            }
        }
        public void Dispose()
        {
            NavStateService.OnNavStateChanged -= StateHasChanged;
        }
        private async Task Logout()
        {
            await JSRuntime.InvokeVoidAsync("sessionStorage.removeItem", "loginExpiry");
            _loginExpiryTime = null;
            NavStateService.SetNavVisibility(false);
            _navigationManager.NavigateTo("/login");
        }

    }
}