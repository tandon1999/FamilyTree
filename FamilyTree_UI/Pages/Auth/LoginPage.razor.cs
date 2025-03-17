using Blazored.Toast.Services;
using FamilyTree_UI.Manager.Interface.Auth;
using FamilyTree_UI.Models.AuthModel;
using FamilyTree_UI.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Diagnostics;

namespace FamilyTree_UI.Pages.Auth
{
    public partial class LoginPage
    {
        public LoginModel loginModel { get; set; } = new();
        [Inject] public IToastService _toastservice { get; set; } = default!;
        [Inject] public NavStateService NavStateService { get; set; } = default!;
        [Inject] public ILoginManager _loginManager { get; set; } = default!;
        [Inject] public NavigationManager _navigationmanager { get; set; } = default!;
        private bool showPassword = false;
        public bool IsNavVisible = false;

        private DateTime? _loginExpiryTime;
        private readonly TimeSpan _sessionDuration = TimeSpan.FromMinutes(60);
        private bool _firstRender = true;
        [Inject] private LoaderService _loader { get; set; } = default!;
        protected override async Task OnInitializedAsync()
        {
            _loader.ShowLoader();
            _loader.HideLoader();
        }
        private async Task HandleLogin()
        {
            if (string.IsNullOrEmpty(loginModel.UserName))
            {
                _toastservice.ShowWarning("Please Enter a Username");
            }
            else if (string.IsNullOrEmpty(loginModel.Password))
            {
                _toastservice.ShowWarning("Please Enter a Password");
            }
            else
            {
                var response = await _loginManager.GetLoginDetails(loginModel);
                if (response.Succeeded)
                {
                    _loginExpiryTime = DateTime.Now.Add(_sessionDuration);
                    await JSRuntime.InvokeVoidAsync("sessionStorage.setItem", "loginExpiry", _loginExpiryTime.ToString());
                    NavStateService.SetNavVisibility(true);
                    _navigationmanager.NavigateTo("/admin");
                    _toastservice.ShowSuccess("Successfully Login!!");
                }
                else
                {
                    _toastservice.ShowWarning(response.Messages);
                }
            }
        }
        private void TogglePasswordVisibility()
        {
            showPassword = !showPassword;
        }
        private async Task Logout()
        {
            await JSRuntime.InvokeVoidAsync("sessionStorage.removeItem", "loginExpiry");
            _loginExpiryTime = null;
            NavStateService.SetNavVisibility(false);
            _navigationmanager.NavigateTo("/login");
            _toastservice.ShowInfo("Session Expired");
        }
    }
}