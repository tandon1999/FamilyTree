using Blazored.Toast.Services;
using FamilyTree_UI.Shared.Services;
using FamilyTree_UI.ViewModels;
using FamilyTreeUI.Manager.Interface;
using FamilyTreeUI.ViewModels;
using Microsoft.AspNetCore.Components;

namespace FamilyTree_UI.Pages.FamilySetups
{
    public partial class FamilyTimeLIne
    {
        public List<TimeLineViewModels> familyTreeMemberlist = new List<TimeLineViewModels>();
        [Inject] public IToastService _toastservice { get; set; } = default!;
        [Inject] public IFamilyTreeMemberManager _familyTreeMemberManager { get; set; } = default!;
        [Inject] private LoaderService _loader { get; set; } = default!;
        [Inject] public NavigationManager _navigatation { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _loader.ShowLoader();
            try
            {
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

        public async Task GetAllFamilyDetails()
        {
            try
            {
                var response = await _familyTreeMemberManager.GetFamilyMemberTimeLine();
                if (response?.Data != null && response.Data.Count > 0)
                {
                    foreach (var member in response.Data)
                    {
                        if (member.ImageByte != null)
                        {
                            member.ImageSrc = "data:image/png;base64," + Convert.ToBase64String(member.ImageByte);
                        }
                        else
                        {
                            _toastservice.ShowWarning("No Image Uploaded!!!");
                        }
                    }
                    familyTreeMemberlist = response.Data;
                }
            }
            catch (Exception ex)
            {
                _toastservice.ShowWarning(ex.Message);
            }
        }
        public async Task Redirecttouserprofile(int Id)
        {
            _navigatation.NavigateTo($"/UserProfile/{Id}", true);
        }
    }
}
