using Blazored.Toast.Services;
using FamilyTree_UI.Configuration.Extension;
using FamilyTree_UI.Manager.Implementation;
using FamilyTree_UI.Manager.Interface;
using FamilyTree_UI.Models;
using FamilyTree_UI.Shared;
using FamilyTree_UI.Shared.Models;
using FamilyTree_UI.Shared.Services;
using FamilyTree_UI.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace FamilyTree_UI.Pages.Blogs
{
    public partial class BlogsSetupPage
    {
        [Inject] public IBlogsManager _blogsmanager { get; set; } = default!;
      //  [Inject] public IToastService _toastservice { get; set; } = default!;
        public BlogsPostModel blogsPost { get; set; } = new();
        public List<BlogsPostVModel> blogsPostlist { get; set; } = new();
        private string uploadedImageUrl;
        public string? Imagesrc;
        private bool ShowModal = false;
        private int RoleId = 0;
        protected override async Task OnInitializedAsync()
        {
            _loader.ShowLoader();
            try
            {
                var currentuser = await _customAuthStateProvider.CurrentUser();
                RoleId = ClaimsPrincipalExtensions.GetRoleId(currentuser);
                if (RoleId != 1)
                {
                    _toastservice.ShowWarning("You are not authorized for this page!!!");
                    _navigationManager.NavigateTo("/");
                    return;
                }
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
            }
            catch (Exception ex)
            {
                _toastservice.ShowError(ex.Message);
            }
        }
        public async Task Save()
        {
            try
            {
                if (string.IsNullOrEmpty(uploadedImageUrl))
                {
                    _toastservice.ShowWarning("Please UPload an Image");
                }
                else if (string.IsNullOrEmpty(blogsPost.Title))
                {
                    _toastservice.ShowWarning("Title is Required");
                }
                else if (string.IsNullOrEmpty(blogsPost.Content))
                {
                    _toastservice.ShowWarning("please describe a Content");
                }
                else
                {
                    var options = new ConfirmDialogOptions
                    {
                        IsVerticallyCentered = true,
                        YesButtonText = "Confirm",
                        YesButtonColor = ButtonColor.Success,
                        NoButtonText = "CANCEL",
                        NoButtonColor = ButtonColor.Danger
                    };
                    var confirmation = await dialog.ShowAsync(
                        title: "Are you sure you want to save this blog?",
                        message1: "This will save the record. Do you want to proceed?",
                        options);
                    if (confirmation)
                    {
                        var response = await _blogsmanager.CreateBlogsPost(blogsPost);
                        if (response.Succeeded)
                        {
                            _toastservice.ShowSuccess(response.Messages);
                            blogsPost = new();
                            uploadedImageUrl = null;
                            await GetAll();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _toastservice.ShowError(ex.Message);
            }
        }
        public async Task Edit(int Id)
        {
            var response = await _blogsmanager.GetBlogsPostById(Id);
            if (response != null)
            {
                if (response.ImageByte != null)
                {
                    uploadedImageUrl = "data:image/png;base64," + Convert.ToBase64String(response.ImageByte);
                }
                else
                {
                    _toastservice.ShowWarning("No Image Uploaded!!!");
                }
                blogsPost = response;
            }
        }
        public async Task Delete(int Id)
        {
            try
            {
                var options = new ConfirmDialogOptions
                {
                    IsVerticallyCentered = true,
                    YesButtonText = "Confirm",
                    YesButtonColor = ButtonColor.Success,
                    NoButtonText = "CANCEL",
                    NoButtonColor = ButtonColor.Danger
                };
                var confirmation = await dialog.ShowAsync(
                    title: "Are you sure you want to delete this blog?",
                    message1: "This will delete the record. Do you want to proceed?",
                    options);
                if (confirmation)
                {
                    var response = await _blogsmanager.DeleteBlogsPost(Id);
                    if (response.Succeeded)
                    {
                        _toastservice.ShowSuccess(response.Messages);
                        await GetAll();
                    }
                }
            }
            catch (Exception ex)
            {
                _toastservice.ShowError(ex.Message);
            }
        }
        private async Task HandleFileSelected(InputFileChangeEventArgs e)
        {
            var _file = e.File;
            if (_file != null)
            {
                var extension = Path.GetExtension(_file.Name);
                var filename = $"{Guid.NewGuid()}{extension}";
                var format = "";
                if (extension == ".pdf")
                {
                    format = "Application/pdf";
                }
                else if (extension == ".png" || extension == ".jpg" || extension == ".jpeg")
                {
                    format = $"image/{extension.TrimStart('.')}";
                }
                using var stream = _file.OpenReadStream(51200000000);
                using var ms = new MemoryStream();
                await stream.CopyToAsync(ms);
                byte[] buffer = ms.ToArray();

                if (format.StartsWith("image/"))
                {
                    var base64 = Convert.ToBase64String(buffer);
                    uploadedImageUrl = $"data:{format};base64,{base64}";
                }
                blogsPost.imageUpload = new ImageUpload { FileByte = buffer, FileName = filename, Extension = extension };
            }
        }

        public async Task Clear()
        {
            blogsPost = new();
            uploadedImageUrl = null;
        }

        public async Task ViewImage(int Id)
        {
            Imagesrc = null;
            var response = blogsPostlist.FirstOrDefault(x => x.Id == Id);
            if (response != null)
            {
                if (response.ImageByte != null)
                {
                    Imagesrc = "data:image/png;base64," + Convert.ToBase64String(response.ImageByte);
                    ShowModal = true;
                }
                else
                {
                    _toastservice.ShowWarning("No Image Uploaded!!!");
                }
            }
        }
        public async Task CloseModal()
        {
            ShowModal = false;
        }
    }
}