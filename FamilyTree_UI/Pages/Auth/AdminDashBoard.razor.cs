using Blazored.Toast.Services;
using FamilyTree_UI.Configuration.Extension;
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
        public DashBoardViewModel dashboardview { get; set; } = new();
        private int RoleId = 0;
        public int selectedTab { get; set; } = 1;
        [Parameter] public EventCallback<string> SelectedTabChanged { get; set; }
        [Parameter] public int tabId { get; set; }
        protected override async Task OnInitializedAsync()
        {
            _loader.ShowLoader();
            try
            {
                var currentuser = await _customAuthStateProvider.CurrentUser();
                RoleId = ClaimsPrincipalExtensions.GetRoleId(currentuser);
                if (RoleId != 1)
                {
                    _navigationManager.NavigateTo("/");
                    _toastservice.ShowWarning("You are not authorized for this page!!!");
                    return;
                }
                await GetAllFamilyDetails();

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
        private void NavigateToTab(int TabId)
        {
            selectedTab = TabId;
            _navigationManager.NavigateTo($"/admin/{TabId}");
            SelectedTabChanged.InvokeAsync(TabId.ToString()); 
        }
        protected override void OnParametersSet()
        {
            if (tabId != 0)
            {
                selectedTab = tabId;
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
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            try
            {
                if (selectedTab == 1 && firstRender == false)
                {
                    await GetAllFamilyDetails();
                    firstRender = true;
                }
                if (firstRender)
                {
                    if (dashboardview?.Generations != null)
                    {
                        var generationData = dashboardview.Generations
                            .Where(g => g.Key.Contains("Generation"))
                            .ToDictionary(g => g.Key, g => g.Value);

                        var maritalData = dashboardview.Generations
                            .Where(g => g.Key.Contains("Marital"))
                            .ToDictionary(g => g.Key, g => g.Value);

                        if (generationData.Any())
                            await RenderChart("generationChart", generationData, "Family Generations Distribution");

                        if (maritalData.Any())
                            await RenderChart("maritalChart", maritalData, "Marital Status Distribution");
                    }
                }
            }
            catch (Exception ex)
            {
                _toastservice.ShowWarning(ex.Message);
            }
        }
        private async Task RenderChart(string canvasId, Dictionary<string, object> dataDict, string chartTitle)
        {
            try
            {
                var labels = dataDict.Keys.Select(key =>
                    {
                        string prefixToRemove = canvasId.Contains("marital", StringComparison.OrdinalIgnoreCase) ? "Marital" : string.Empty;
                        return key.StartsWith(prefixToRemove) ? key.Substring(prefixToRemove.Length) : key;
                    }).ToArray();
                var data = dataDict.Values.ToArray();
                var backgroundColors = GenerateColors(data.Length);

                var chartConfig = new
                {
                    type = "pie",
                    data = new
                    {
                        labels = labels,
                        datasets = new[]
                        {
                new
                {
                    label = chartTitle,
                    data = data,
                    backgroundColor = backgroundColors,
                    borderColor = "rgba(255,255,255,1)",
                    borderWidth = 1,
                    hoverOffset = 10
                }
                }
                    },
                    options = new
                    {
                        responsive = true,
                        maintainAspectRatio = false,
                        plugins = new
                        {
                            legend = new
                            {
                                position = "bottom"
                            },
                            title = new
                            {
                                display = true,
                                text = chartTitle
                            }
                        }
                    }
                };

                await _js.InvokeVoidAsync("renderChart", canvasId, chartConfig);
            }
            catch (Exception ex)
            {
                _toastservice.ShowWarning(ex.Message);
            }
        }

        private string[] GenerateColors(int count)
        {
            // Predefined base colors (can be expanded)
            string[] baseColors = new[]
            {
                "rgba(255, 99, 132, 0.6)",   // red
                "rgba(54, 162, 235, 0.6)",   // blue
                "rgba(255, 206, 86, 0.6)",   // yellow
                "rgba(75, 192, 192, 0.6)",   // teal
                "rgba(153, 102, 255, 0.6)",  // purple
                "rgba(255, 159, 64, 0.6)",   // orange
                "rgba(201, 203, 207, 0.6)",  // grey
                "rgba(100, 181, 246, 0.6)",  // light blue
                "rgba(255, 138, 101, 0.6)",  // coral
                "rgba(174, 213, 129, 0.6)",  // light green
                "rgba(255, 112, 67, 0.6)",   // deep orange
                "rgba(124, 179, 66, 0.6)",   // olive green
                "rgba(255, 202, 40, 0.6)",   // amber
                "rgba(63, 81, 181, 0.6)",    // indigo
                "rgba(0, 188, 212, 0.6)"     // cyan
            };

            return Enumerable.Range(0, count)
                             .Select(i => baseColors[i % baseColors.Length])
                             .ToArray();
        }
        private string GetCardColor(string key)
        {
            return key switch
            {
                "TotalFamilyMember" => "#F08080", // Light Coral
                "AverageLifespan" => "#87CEFA", // Light Sky Blue
                "LongestLivingIndividual" => "#ADFF2F", // Green Yellow (a softer vibrant green)
                "Death" => "#CD5C5C", // Indian Red (a muted red)
                "Living" => "#98FB98", // Pale Green (a gentle green)
                "Female" => "#FFB6C1", // Light Pink (softer pink)
                "Male" => "#6495ED", // Cornflower Blue (a medium, less intense blue)
                _ => "#E6E6FA" // Lavender (a soft, light purple)
            };
        }
        private async Task Navigate()
        {
            NavigateToTab(3);
        }
    }
}