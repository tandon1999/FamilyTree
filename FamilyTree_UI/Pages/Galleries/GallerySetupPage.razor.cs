using Blazored.Toast.Services;
using FamilyTree_UI.Configuration.Extension;
using FamilyTree_UI.Manager.Interface;
using FamilyTree_UI.Models;
using FamilyTree_UI.Shared;
using FamilyTree_UI.Shared.Models;
using FamilyTree_UI.Shared.Services;
using FamilyTree_UI.ViewModels;
using FamilyTreeUI.Manager.Implementation;
using FamilyTreeUI.Manager.Interface;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Text.Json;

namespace FamilyTree_UI.Pages.Galleries
{
    public partial class GallerySetupPage
    {
        
        [Inject] public ISetupPagesManager _setuppagesmanager { get; set; } = default!;
        public GallerySetupModel gallerysetupmodel { get; set; } = new();
        public List<GallerySetupModel> gallerysetupmodellist { get; set; } = new();
        public List<GallerySetupVModel> gallerySetupslist { get; set; } = new();
        private string uploadedImageUrl;
        private int count;
        public string Imagesrc { get; set; }
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
        private async Task CreateGallerySetup(int type)
        {
            if (type == 1)
            {
                if (string.IsNullOrEmpty(uploadedImageUrl))
                {
                    _toastservice.ShowWarning("Please Upload a Image");
                    return;
                }
                else if (string.IsNullOrEmpty(gallerysetupmodel.PhotoName))
                {
                    _toastservice.ShowWarning("Photo Name is required");
                    return;
                }
                else if (gallerysetupmodel.Category == 0)
                {
                    _toastservice.ShowWarning("Category is required");
                    return;
                }
                else if (string.IsNullOrEmpty(gallerysetupmodel.Description))
                {
                    _toastservice.ShowWarning("Description is required");
                    return;
                }
                else
                {

                    if (gallerysetupmodel.TempId == 0)
                    {
                        gallerysetupmodel.TempId = ++count;
                    }
                    else
                    {
                        gallerysetupmodellist.RemoveAll(x => x.TempId == gallerysetupmodel.TempId);
                    }
                }
                gallerysetupmodellist.Add(gallerysetupmodel);
                gallerysetupmodel = new();
                uploadedImageUrl = null;
            }
            else if (type == 2)
            {
                if (gallerysetupmodellist.Count < 0)
                {
                    _toastservice.ShowWarning("Please Add atleast one image");
                    return;
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
                        title: "Are you sure you want to save this galleries?",
                        message1: "This will save the record. Do you want to proceed?",
                        options);
                    if (confirmation)
                    {
                        gallerysetupmodel.gallerysetupList = gallerysetupmodellist;
                        var response = await _setuppagesmanager.CreateGallerySetup(gallerysetupmodel);
                        if (response.Succeeded)
                        {
                            _toastservice.ShowSuccess(response.Messages);
                            gallerysetupmodel = new();
                            gallerysetupmodellist.Clear();
                            uploadedImageUrl = null;
                            await GetAll();
                        }
                    }
                }
            }
        }
        public async Task ViewImage(int Id, int type)
        {
            try
            {
                if (type == 1)
                {
                    Imagesrc = null;
                    var response = gallerysetupmodellist.FirstOrDefault(x => x.TempId == Id);
                    if (response != null)
                    {
                        if (response.imageUpload.FileByte != null)
                        {
                            Imagesrc = "data:image/png;base64," + Convert.ToBase64String(response.imageUpload.FileByte);
                            ShowModal = true;
                        }
                        else
                        {
                            _toastservice.ShowWarning("No Image Uploaded!!!");
                        }
                    }
                }
                else if (type == 2)
                {
                    Imagesrc = null;
                    var response = gallerySetupslist.FirstOrDefault(x => x.Id == Id);
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
            }
            catch (Exception ex)
            {
                _toastservice.ShowWarning(ex.Message);
            }
        }
        public async Task Edit(int Id, int type)
        {
            if (type == 1)
            {

                var response = gallerysetupmodellist.FirstOrDefault(x => x.TempId == Id);
                if (response != null)
                {
                    gallerysetupmodel.Description = response.Description;
                    gallerysetupmodel.DateofPhoto = response.DateofPhoto;
                    gallerysetupmodel.TempId = response.TempId;
                    gallerysetupmodel.PhotoName = response.PhotoName;
                    gallerysetupmodel.imageUpload = response.imageUpload;
                }
            }
            else if (type == 2)
            {
                var response = await _setuppagesmanager.GetGalleryImageById(Id);
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
                    gallerysetupmodel = response;
                }
            }
        }
        public async Task Delete(int Id, int type)
        {
            try
            {
                if (type == 1)
                {
                    gallerysetupmodellist.RemoveAll(x => x.TempId == Id);
                }
                else if (type == 2)
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
                        title: "Are you sure you want to delete this galeries?",
                        message1: "This will delete the record. Do you want to proceed?",
                        options);
                    if (confirmation)
                    {
                        var response = await _setuppagesmanager.DeleteGalleryImage(Id);
                        if (response.Succeeded)
                        {
                            _toastservice.ShowSuccess(response.Messages);
                            await GetAll();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _toastservice.ShowSuccess(ex.Message);
            }
        }
        private void CloseModal()
        {
            ShowModal = false;
            Imagesrc = null;
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
                gallerysetupmodel.imageUpload = new ImageUpload { FileByte = buffer, FileName = filename, Extension = extension };
            }
        }
        public async Task Clear()
        {
            uploadedImageUrl = null;
            gallerysetupmodel = new();
        }
    }
}