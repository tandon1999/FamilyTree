using Blazored.Toast.Services;
using FamilyTree_UI.Manager.Interface;
using FamilyTree_UI.Shared;
using FamilyTree_UI.Shared.Models;
using FamilyTree_UI.Shared.Services;
using FamilyTree_UI.ViewModels;
using FamilyTreeUI.Manager.Interface;
using FamilyTreeUI.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
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
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await RenderChart();
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
        private async Task RenderChart()
        {
            await Task.Delay(500);
            if (dashboardview?.Generations != null)
            {
                var generations = dashboardview.Generations
                                .Where(g => g.Key.Contains("Generation"))
                                .ToDictionary(g => g.Key, g => g.Value);
                var labels = generations.Keys.ToArray();
                var data = generations.Values.ToArray();
                var chartConfig = new
                {
                    type = "bar",
                    data = new
                    {
                        labels = labels,
                        datasets = new[]
                        {
                    new
                    {
                        label = "Generations",
                        data = data,
                        backgroundColor = "rgba(54, 162, 235, 0.2)",
                        borderColor = "rgba(54, 162, 235, 1)",
                        borderWidth = 1
                    }
                }
                    },
                    options = new
                    {
                        scales = new
                        {
                            y = new
                            {
                                beginAtZero = true
                            }
                        }
                    }
                };
                await JS.InvokeVoidAsync("renderChart", chartConfig);
            }
        }


    }
}