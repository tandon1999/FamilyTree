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
        [Inject] public ILoginManager _loginManager { get; set; } = default!;
        private bool showPassword = false;
        public bool IsNavVisible = false;

        private DateTime? _loginExpiryTime;
        private readonly TimeSpan _sessionDuration = TimeSpan.FromMinutes(60);
        private bool _firstRender = true;
        protected override async Task OnInitializedAsync()
        {
            _loader.ShowLoader();
            _loader.HideLoader();
        }
        private async Task HandleLogin()
        {
            try
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
                        await _customAuthStateProvider.UpadteAuthenticationState(response.Data);
                        if (response.Data.RoleId == 1)
                        {
                            _navigationManager.NavigateTo("/admin", true);
                        }
                        else if (response.Data.RoleId != 1)
                        {
                            _navigationManager.NavigateTo("/");
                        }
                        _toastservice.ShowSuccess("Successfully Login!!");
                    }
                    else
                    {
                        _toastservice.ShowWarning(response.Messages);
                    }
                }
            }
            catch (Exception ex)
            {

                _toastservice.ShowWarning(ex.Message);
            }
        }
        private void TogglePasswordVisibility()
        {
            showPassword = !showPassword;
        }
    }
}