using Blazored.Toast.Services;
using FamilyTree_UI.Configuration.Extension;
using FamilyTree_UI.Manager.Interface.Auth;
using FamilyTree_UI.Models.AuthModel;
using FamilyTree_UI.Shared.Models;
using FamilyTree_UI.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace FamilyTree_UI.Shared
{
    public partial class NavMenu
    {
        private bool collapseNavMenu = true;
        private string? NavMenuCssClass => collapseNavMenu ? "collapse" : "show";
        
        private DateTime? _loginExpiryTime;
        private readonly TimeSpan _sessionDuration = TimeSpan.FromMinutes(1);
        [Parameter] public DateTime? LoginExpiry { get; set; }
        private bool IsLoggedIn = false;
        private int RoleId = 0;
        private async Task ToggleNavMenu()
        {
            try
            {
                collapseNavMenu = !collapseNavMenu;
            }
            catch (Exception ex)
            {
                _toastservice.ShowError(ex.Message);
            }
        }
        protected async override Task OnInitializedAsync()
        {
            _loader.ShowLoader();
            try
            {
                var session = await _protectedLocalStorage.GetAsync<LoginTokenModel>("Token");
                IsLoggedIn = session.Success && session.Value != null;
                var currentuser = await _customAuthStateProvider.CurrentUser();
                RoleId = ClaimsPrincipalExtensions.GetRoleId(currentuser);
                StateHasChanged();
            }
            catch (Exception ex)
            {
               _toastservice.ShowError(ex.Message);
            }
            finally
            {
                _loader.HideLoader();
            }
        }
        private async Task Logout()
        {
            var options = new ConfirmDialogOptions
            {
                IsVerticallyCentered = true,
                YesButtonText = "Confirm",
                YesButtonColor = ButtonColor.Success,
                NoButtonText = "CANCEL",
                NoButtonColor = ButtonColor.Danger
            };
            var confirmation = await dialog.ShowAsync(
                title: "Logout",
                message1: "Are you sure want to logout?",
                options);
            if (confirmation)
            {
                var session = await _protectedLocalStorage.GetAsync<LoginTokenModel>("Token");
                var customAuthenticationStateProvider = (CustomAuthenticationStateProvider)_authStateProvider;
                await customAuthenticationStateProvider.UpadteAuthenticationState(null);
                customAuthenticationStateProvider.MarkUserAsLoggedOut();
                await _protectedLocalStorage.DeleteAsync("Token");
                IsLoggedIn = false;
                _navigationManager.NavigateTo("/login", forceLoad: true);
            }
        }
    }
}