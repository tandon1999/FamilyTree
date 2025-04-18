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
            }
            finally
            {
                _loader.HideLoader();
                StateHasChanged();
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
            }
            catch (Exception ex)
            {
                _toastservice.ShowWarning(ex.Message);
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

    }
}