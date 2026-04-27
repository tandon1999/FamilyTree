using Blazored.Toast.Services;
using FamilyTree_UI.Manager.Interface;
using FamilyTree_UI.Shared.Services;
using FamilyTree_UI.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace FamilyTree_UI.Pages.Blogs
{
    public partial class BlogPage
    {
        [Inject] public IBlogsManager _blogsmanager { get; set; } = default!;
        public List<BlogsPostVModel> blogsPostlist { get; set; } = new();
        private string uploadedImageUrl;
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
                var response = await _blogsmanager.GetAllBlogsPost();
                if (response?.Data != null && response.Data.Count > 0)
                {
                    foreach (var image in response.Data)
                    {
                        if (image.ImageByte != null)
                        {
                            image.ImageSrc = "data:image/png;base64," + Convert.ToBase64String(image.ImageByte);
                        }

                    }
                    blogsPostlist = response.Data;
                }

                _pendingImageCount = blogsPostlist.Count(x => !string.IsNullOrWhiteSpace(x.ImageSrc));
                if (_pendingImageCount == 0)
                {
                    _loader.HideLoader();
                }
            }
            catch (Exception ex)
            {
                _toastservice.ShowError(ex.Message);
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

        public async Task GotoBlogDetails(int Id)
        {
            _navigationManager.NavigateTo($"blogdetails/{Id}");
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await _js.InvokeVoidAsync("googleTranslateElementInit");
            }
        }
    }
}