using Blazored.Toast.Services;
using FamilyTree_UI.Manager.Interface;
using FamilyTree_UI.Shared.Services;
using FamilyTree_UI.ViewModels;
using Microsoft.AspNetCore.Components;

namespace FamilyTree_UI.Pages.Galleries
{
    public partial class GetGalleryPages
    {
        [Inject] public ISetupPagesManager _setuppagesmanager { get; set; } = default!;
        public List<GallerySetupVModel> gallerySetupslist { get; set; } = new();
        private int _pendingImageCount;
        
        protected override async Task OnInitializedAsync()
        {
            _loader.ShowLoader();
            try
            {
                await GetAll();
            }
            catch (Exception ex)
            {
                _toastservice.ShowWarning(ex.Message);
                _loader.HideLoader();
            }
        }

        public async Task GetAll()
        {
            try
            {
                var response = await _setuppagesmanager.GetAllGalleries();
                if (response?.Data != null && response.Data.Count > 0)
                {
                    foreach (var image in response.Data)
                    {
                        if (image.ImageByte != null)
                        {
                            image.ImageSrc = "data:image/png;base64," + Convert.ToBase64String(image.ImageByte);
                        }
                    }
                    gallerySetupslist = response.Data;
                }

                _pendingImageCount = gallerySetupslist.Count(x => !string.IsNullOrWhiteSpace(x.ImageSrc));
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



        private void ShowDetails(GallerySetupVModel image)
        {
            image.IsDetailsVisible = true;
        }

        private void HideDetails(GallerySetupVModel image)
        {
            image.IsDetailsVisible = false;
        }

        private string GetCategoryClass(string? categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
            {
                return "category-default";
            }

            var key = categoryName.Trim().ToLowerInvariant();
            return key switch
            {
                "dashain" => "category-dashain",
                "tihar" => "category-tihar",
                "marriage" => "category-marriage",
                "others" => "category-others",
                _ => "category-default"
            };
        }

        private string GetCategoryLabel(string? categoryName)
        {
            return string.IsNullOrWhiteSpace(categoryName) ? "Moments" : categoryName.Trim();
        }

    }
}