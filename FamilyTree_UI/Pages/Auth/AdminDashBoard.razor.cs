using Blazored.Toast.Services;
using FamilyTree_UI.Manager.Interface;
using FamilyTree_UI.Shared;
using FamilyTree_UI.Shared.Models;
using FamilyTree_UI.Shared.Services;
using FamilyTree_UI.ViewModels;
using FamilyTreeUI.Manager.Interface;
using FamilyTreeUI.ViewModels;
using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace FamilyTree_UI.Pages.Auth
{
    public partial class AdminDashBoard
    {
        [Inject] public IDashBoardManager _dashboardmanager { get; set; } = default!;
        [Inject] public IToastService _toastservice { get; set; } = default!;
        [Inject] public NavigationManager _navigationManager { get; set; } = default!;
        public DashBoardViewModel dashboardview { get; set; } = new();
        [Inject] private LoaderService _loader { get; set; } = default!;
        protected override async Task OnInitializedAsync()
        {
            _loader.ShowLoader();
            try
            {
                if (!NavStateService.IsNavVisible)
                {
                    _toastservice.ShowWarning("You are not authorized for this page!!!");
                    _navigationManager.NavigateTo("/");
                    return;
                }
                else
                {
                    await GetAllFamilyDetails();
                }
            }
            catch (Exception ex)
            {
                _toastservice.ShowWarning(ex.Message);
            }
            finally
            {
                _loader.HideLoader();
            }
        }

        public async Task GetAllFamilyDetails()
        {
            try
            {
                var response = await _dashboardmanager.GetDashboardData();
                if (response != null)
                {
                    var data = JsonSerializer.Deserialize<Dictionary<string, object>>(response.DashBoardData);

                    // Set the dashboard view model
                    dashboardview = new DashBoardViewModel
                    {
                        Generations = data
                    };
                }
            }
            catch (Exception ex)
            {
                _toastservice.ShowWarning(ex.Message);
            }
        }

    }
}