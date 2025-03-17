using Blazored.Toast.Services;
using FamilyTree_UI.Shared.Component;
using FamilyTree_UI.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace FamilyTree_UI.Shared
{
    public partial class MainLayout
    {
        [Inject] public NavStateService NavStateService { get; set; }
        private DateTime? _loginExpiryTime;
        private readonly TimeSpan _sessionDuration = TimeSpan.FromMinutes(1);
        [Inject] public IToastService _toastservice { get; set; } = default!;
        [Inject] public NavigationManager _navigationManager { get; set; } = default!;
        private Loader loader;
        [Inject] private LoaderService LoaderService { get; set; }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && !hasSetLanguage)
            {
                await CheckSession();
                if (loader != null)
                {
                    LoaderService.RegisterShowAction(loader.ShowLoader);
                    LoaderService.RegisterHideAction(loader.HideLoader);

                    selectedLanguage = await JSRuntime.InvokeAsync<string>("localStorage.getItem", "selectedLanguage") ?? "en";                
                    StateHasChanged();
                }
            }
        }


        /* private async Task CheckSession()
         {
             var expiryString = await JSRuntime.InvokeAsync<string>("sessionStorage.getItem", "loginExpiry");
             if (!string.IsNullOrEmpty(expiryString) && DateTime.TryParse(expiryString, out DateTime expiryTime))
             {
                 if (DateTime.Now < expiryTime)
                 {
                     _loginExpiryTime = expiryTime;
                     NavStateService.SetNavVisibility(true);
                 }
                 else
                 {
                     _toastservice.ShowInfo("Session Expired");
                     await Logout();

                 }
             }
             else
             {
                 NavStateService.SetNavVisibility(false);
                 _navigationManager.NavigateTo("/");
             }
         }*/
        private async Task CheckSession()
        {
            var expiryString = await JSRuntime.InvokeAsync<string>("sessionStorage.getItem", "loginExpiry");
            var currentUrl = _navigationManager.Uri;
            if (currentUrl.Contains("UserProfile"))
            {
                NavStateService.SetNavVisibility(false);
                return;
            }

            if (!string.IsNullOrEmpty(expiryString) && DateTime.TryParse(expiryString, out DateTime expiryTime))
            {
                if (DateTime.Now < expiryTime)
                {
                    _loginExpiryTime = expiryTime;
                    NavStateService.SetNavVisibility(true);
                }
                else
                {
                    _toastservice.ShowInfo("Session Expired");
                    await Logout();
                }
            }
            else
            {
                NavStateService.SetNavVisibility(false);
                _navigationManager.NavigateTo("/");
            }
        }

        private async Task Logout()
        {
            await JSRuntime.InvokeVoidAsync("sessionStorage.removeItem", "loginExpiry");
            _loginExpiryTime = null;
            NavStateService.SetNavVisibility(false);
            _navigationManager.NavigateTo("/");
        }

        private bool hasSetLanguage = false;

        private string selectedLanguage = "en";
        private async Task ChangeLanguage(ChangeEventArgs e)
        {
            selectedLanguage = e.Value.ToString();
            await JSRuntime.InvokeVoidAsync("localStorage.setItem", "selectedLanguage", selectedLanguage);
            StateHasChanged();
        }

    }
}