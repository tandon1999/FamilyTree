using Blazored.Toast.Services;
using FamilyTree_UI.Shared.Services;
using FamilyTreeUI.Manager.Interface;
using FamilyTreeUI.ViewModels;
using Microsoft.AspNetCore.Components;

namespace FamilyTree_UI.Pages.FamilySetups
{
    public partial class MembersDictionaryPage
    {
        
        [Inject] public IFamilyTreeMemberManager _familyTreeMemberManager { get; set; } = default!;
        public FamilyTreeMemberVModel familyTreeMembervmodel { get; set; } = new();
        public string Imagesrc { get; set; }
        private int _pendingImageCount;
        string nameFilter = string.Empty;
        public IQueryable<FamilyTreeMemberVModel>? _gridData { get; set; }

        IQueryable<FamilyTreeMemberVModel>? familyTreeMemberlist => _gridData?
            .Where(x => x.FirstName.ToLower().Contains(nameFilter.ToLower()));
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
                    _gridData = response.Data.AsQueryable();
                }

                _pendingImageCount = _gridData?.Count(x => !string.IsNullOrWhiteSpace(x.ImageSrc)) ?? 0;
                if (_pendingImageCount == 0)
                {
                    _loader.HideLoader();
                }

            }
            catch (Exception ex)
            {
                _toastservice.ShowWarning(ex.Message);
                _loader.HideLoader();
            }
        }

        private void HandleImageLoadComplete()
        {
            if (_pendingImageCount <= 0)
            {
                return;
            }

            _pendingImageCount--;
            if (_pendingImageCount == 0)
            {
                _loader.HideLoader();
            }
        }

        public async Task Redirecttouserprofile(int Id)
        {
            _navigationManager.NavigateTo($"/UserProfile/{Id}");
        }    
    }
}