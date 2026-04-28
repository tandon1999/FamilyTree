using AutoMapper;
using Blazored.Toast.Services;
using FamilyTree_UI.Manager.Interface;
using FamilyTree_UI.Models;
using FamilyTree_UI.Shared.Services;
using FamilyTree_UI.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace FamilyTree_UI.Pages.Blogs
{
    public partial class BlogDetailsPage
    {
        [Parameter] public int Id { get; set; }
        public BlogsPostModel blog { get; set; } = new();
        [Inject] public IBlogsManager _blogsmanager { get; set; } = default!;
      //  [Inject] public IToastService _toastservice { get; set; } = default!;
        public string Imagesrc;
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
            }
        }
        public async Task GetAll()
        {
            try
            {
                var response = await _blogsmanager.GetBlogsPostById(Id);
                if (response != null)
                {
                    if (response.ImageByte != null)
                    {
                        Imagesrc = "data:image/png;base64," + Convert.ToBase64String(response.ImageByte);
                    }
                    else
                    {
                        _toastservice.ShowWarning("No Image Uploaded!!!");
                    }
                }
                blog = response;
            }
            catch (Exception ex)
            {
                _toastservice.ShowError(ex.Message);
            }
        }

        public string GetReadTimeText()
        {
            if (string.IsNullOrWhiteSpace(blog?.Content))
            {
                return "Quick read";
            }

            var wordCount = blog.Content.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
            var minutes = Math.Max(1, (int)Math.Ceiling(wordCount / 180.0));
            return $"{minutes} min read";
        }

        private async Task ScrollToStory()
        {
            await _js.InvokeVoidAsync("scrollToElement", "story");
        }
    }
}