using Blazored.Toast.Services;
using FamilyTree_UI.Shared.Services;
using FamilyTreeUI.Manager.Interface;
using FamilyTreeUI.ViewModels;
using Microsoft.AspNetCore.Components;

namespace FamilyTree_UI.Pages.FamilySetups
{
    public partial class MembersDictionaryPage
    {
        [Inject] public IToastService _toastservice { get; set; } = default!;
        [Inject] public IFamilyTreeMemberManager _familyTreeMemberManager { get; set; } = default!;
        public FamilyTreeMemberVModel familyTreeMembervmodel { get; set; } = new();
        [Inject] public NavigationManager _navigatation { get; set; }

        public List<FamilyTreeMemberVModel> familyTreeMemberlist { get; set; } = new();
        public string Imagesrc { get; set; }
        [Inject] private LoaderService _loader { get; set; } = default!;
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
                var response = await _familyTreeMemberManager.GetFamilyTreeMembers(0);
                if (response?.Data != null && response.Data.Count > 0)
                {
                    foreach (var image in response.Data)
                    {
                        if (image.ImageByte != null)
                        {
                            image.ImageSrc = "data:image/png;base64," + Convert.ToBase64String(image.ImageByte);
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
            _navigatation.NavigateTo($"/UserProfile/{Id}");
        }

    }
}