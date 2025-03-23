using Blazored.Toast.Services;
using FamilyTree_UI.Shared;
using FamilyTree_UI.Shared.Models;
using FamilyTree_UI.Shared.Services;
using FamilyTreeUI.Manager.Interface;
using FamilyTreeUI.Models;
using FamilyTreeUI.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace FamilyTree_UI.Pages.FamilySetups
{
    public partial class LIstofFamilyTree
    {
        [Inject] public IFamilyTreeMemberManager _familyTreeMemberManager { get; set; } = default!;
        [Inject] public IToastService _toastservice { get; set; } = default!;
        [Inject] public NavigationManager _navigationManager { get; set; } = default!;
        [Inject] public NavStateService NavStateService { get; set; }
        public FamilyMemberSetupModel memberSetupModel { get; set; } = new();
        public FamilyTreeMemberVModel familyTreeMembervmodel { get; set; } = new();
        //  public List<FamilyTreeMemberVModel> familyTreeMemberlist { get; set; } = new();
        public string Imagesrc { get; set; }
        private string uploadedImageUrl;
        private DateTime dob;
        private bool ShowModal = false;
        private bool ShowModal1 = false;
        string nameFilter = string.Empty;
        public IQueryable<FamilyTreeMemberVModel>? _gridData { get; set; }

        IQueryable<FamilyTreeMemberVModel>? familyTreeMemberlist => _gridData?
            .Where(x => x.FirstName.ToLower().Contains(nameFilter.ToLower()));

        [Inject] private LoaderService _loader { get; set; } = default!;
        protected override async Task OnInitializedAsync()
        {
            _loader.ShowLoader();
            try
            {
                if (!NavStateService.IsNavVisible)
                {
                    _toastservice.ShowWarning("You are not authorized for this page!!!");
                    _navigationManager.NavigateTo("/");
                    return;
                }
                else
                {
                    await GetAllFamilyDetails();
                }
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
                _gridData = null;
                var response = await _familyTreeMemberManager.GetFamilyTreeMembers(familyTreeMembervmodel.Id);
                if (response?.Data != null && response.Data.Count > 0)
                {
                    _gridData = response.Data.AsQueryable();
                }
            }
            catch (Exception ex)
            {
                _toastservice.ShowWarning(ex.Message);
            }
        }
        /*public async Task Filter(string Id)
        {
            if (int.Parse(Id) != 0)
            {
                familyTreeMembervmodel.Id = int.Parse(Id);
            }
            await GetAllFamilyDetails();
        }*/
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
                    title: "Are you sure you want to delete this member?",
                    message1: "This will delete the record. Do you want to proceed?",
                    options);
                if (confirmation)
                {
                    var response = await _familyTreeMemberManager.DeleteFamilyTreeMember(Id);
                    if (response.Succeeded)
                    {
                        _toastservice.ShowSuccess(response.Messages);
                        await GetAllFamilyDetails();
                    }
                }
            }
            catch (Exception ex)
            {
                _toastservice.ShowWarning(ex.Message);
            }
        }
        public async Task Edit(int Id)
        {
            try
            {
                var response = await _familyTreeMemberManager.GetFamilyTreeMemberByid(Id);
                memberSetupModel = response;
                memberSetupModel.IsDeath = memberSetupModel.DeathDate.HasValue;
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
                }
                ShowModal1 = true;
            }
            catch (Exception ex)
            {
                _toastservice.ShowWarning(ex.Message);
            }
        }
        public async Task ViewImage(int Id)
        {
            try
            {
                Imagesrc = null;
                var response = await _familyTreeMemberManager.GetFamilyTreeMemberByid(Id);
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
            catch (Exception ex)
            {
                _toastservice.ShowWarning(ex.Message);
            }
        }
        public async Task AddNewFamilyMember()
        {
            _navigationManager.NavigateTo("/familysetup");
            StateHasChanged();
        }
        private void CloseModal()
        {
            if (ShowModal)
            {
                ShowModal = false;
            }
            else
            {
                ShowModal1 = false;
            }
        }
        public async Task Save()
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
                    title: "Are you sure you want to update this member?",
                    message1: "This will update the record. Do you want to proceed?",
                    options);
                if (confirmation)
                {
                    var response = await _familyTreeMemberManager.CreateFamilyTreeMember(memberSetupModel);
                    if (response.Succeeded)
                    {
                        _toastservice.ShowSuccess(response.Messages);
                        memberSetupModel = new();
                        ShowModal1 = false;
                        uploadedImageUrl = null;
                        await GetAllFamilyDetails();
                    }
                }
            }
            catch (Exception ex)
            {
                _toastservice.ShowWarning(ex.Message);
            }
        }
        private async Task CalculateAge(ChangeEventArgs e, int type)
        {
            if (DateTime.TryParse(e.Value.ToString(), out DateTime dob))
            {
                if (type == 1)
                {
                    memberSetupModel.DOB = dob;
                }
                else if (type == 2)
                {
                    memberSetupModel.DeathDate = dob;
                }
                if (memberSetupModel.DeathDate.HasValue)
                {
                    memberSetupModel.Age = await CalculateAgeBetweenDates(memberSetupModel.DOB, memberSetupModel.DeathDate.Value);
                }
                else
                {
                    memberSetupModel.Age = await CalculateAgeFromDateOfBirth(dob);
                }
            }
        }

        private async Task<int> CalculateAgeFromDateOfBirth(DateTime birthDate)
        {
            DateTime today = DateTime.Today;
            return await CalculateAgeBetweenDates(birthDate, today);
        }

        private async Task<int> CalculateAgeBetweenDates(DateTime birthDate, DateTime endDate)
        {
            int age = endDate.Year - birthDate.Year;

            if (birthDate.Date > endDate.AddYears(-age))
            {
                age--;
            }
            return age;
        }
        private async Task ClearDate()
        {
            if (memberSetupModel.IsDeath == true)
            {
                memberSetupModel.DeathDate = null;
            }
            memberSetupModel.Age = await CalculateAgeFromDateOfBirth(memberSetupModel.DOB);
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
                memberSetupModel.imageUpload = new ImageUpload { FileByte = buffer, FileName = filename, Extension = extension };
            }
        }
    }
}