using Blazored.Toast.Services;
using FamilyTree_UI.Manager.Interface;
using FamilyTree_UI.ViewModels;
using FamilyTreeUI.Manager.Interface;
using FamilyTreeUI.ViewModels;
using Microsoft.AspNetCore.Components;

namespace FamilyTree_UI.Pages
{
    public partial class Index
    {
        private string searchQuery = "";
        
        [Inject] public IFamilyTreeMemberManager _familyTreeMemberManager { get; set; } = default!;
        [Inject] public IDashBoardManager _dashManager { get; set; } = default!;
        public List<UpcommingAnniVModel> UpcommingAnniiversarylist { get; set; } = new();
        public List<LastestBlogPostVModel> LastestBlogPostlist { get; set; } = new();
        string nameFilter = string.Empty;
        public IQueryable<FamilyTreeMemberVModel>? _gridData { get; set; }

        IQueryable<FamilyTreeMemberVModel>? familyTreeMemberlist => _gridData?
            .Where(x => x.FirstName.ToLower().Contains(nameFilter.ToLower()));
        private async Task Navigate(int type)
        {
            if (type == 1)
            {
                _navigationManager.NavigateTo("/members-dictionary");
            }
            else if (type == 2)
            {
                _navigationManager.NavigateTo("/familytree");
            }
            else if (type == 3)
            {
                _navigationManager.NavigateTo("/Timeline");
            }
            else if (type == 4)
            {
                _navigationManager.NavigateTo("/Getgallery");
            }
            else if (type == 5)
            {
                _navigationManager.NavigateTo("/blog");
            }
        }
        public async Task GotoBlogDetails(int Id)
        {
            _navigationManager.NavigateTo($"blogdetails/{Id}");
        }
        protected override async Task OnInitializedAsync()
        {
            _loader.ShowLoader();
            try
            {
                await GetUpcommingAnniversary();
                await GetLatestBlogs();
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
        public async Task GetUpcommingAnniversary()
        {
            try
            {
                var response = await _dashManager.GetUpcommingAnniversary();
                if (response?.Data != null && response?.Data.Count > 0)
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
                    UpcommingAnniiversarylist = response.Data;
                }
            }
            catch (Exception ex)
            {

                _toastservice.ShowWarning(ex.Message);
            }
        }
        public async Task GetLatestBlogs()
        {
            try
            {

                var response = await _dashManager.GetLatestBlogsPost();
                if (response?.Data != null && response?.Data.Count > 0)
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
                    LastestBlogPostlist = response.Data;
                }
            }
            catch (Exception ex)
            {

                _toastservice.ShowWarning(ex.Message);
            }
        }

        public async Task GetAllFamilyDetails()
        {
            try
            {
                var response = await _familyTreeMemberManager.GetFamilyTreeMembers(0);
                if (response?.Data != null && response.Data.Count > 0)
                {
                    foreach (var images in response.Data)
                    {

                        if (images.ImageByte != null)
                        {
                            images.ImageSrc = "data:image/png;base64," + Convert.ToBase64String(images.ImageByte);
                        }
                        else
                        {
                            _toastservice.ShowWarning("No Image Uploaded!!!");
                        }
                        _gridData = response.Data.AsQueryable();
                    }
                }
            }
            catch (Exception ex)
            {
                _toastservice.ShowWarning(ex.Message);
            }
        }
        public async Task CheckName()
        {
            if (!string.IsNullOrEmpty(nameFilter))
            {
                await GetAllFamilyDetails();
            }
            else
            {
                _gridData = null;
            }
        }
        public async Task Redirecttouserprofile(int Id)
        {
            _navigationManager.NavigateTo($"/UserProfile/{Id}");
        }
        private void SetLanguage(string culture)
        {
            var cultureInfo = new System.Globalization.CultureInfo(culture);
            System.Globalization.CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
        }
    }
}