using FamilyTreeUI.Manager.Interface;
using FamilyTreeUI.Models;
using FamilyTreeUI.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics.CodeAnalysis;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components.Forms;
using FamilyTree_UI.Shared;
using FamilyTree_UI.Models;
using FamilyTree_UI.Shared.Services;
using FamilyTree_UI.Shared.Models;

namespace FamilyTree_UI.Pages.FamilySetups
{
    public partial class FamilyTreeMemberSetupPage
    {
        [Inject] public IFamilyTreeMemberManager _familyTreeMemberManager { get; set; } = default!;
        [Inject] public IToastService _toastservice { get; set; } = default!;
        [Inject] public NavStateService NavStateService { get; set; }
        [Inject] public NavigationManager _navigationManager { get; set; } = default!;
        public FamilyMemberSetupModel memberSetupModel { get; set; } = new();
        public FamilyTreeMemberVModel familyTreeMembervmodel { get; set; } = new();
        public List<FamilyTreeMemberVModel> familyTreeMemberlist { get; set; } = new();
        private int age;
        private DateTime dob;
        private string uploadedImageUrl;
        [Inject] private LoaderService _loader { get; set; } = default!;
        protected override async Task OnInitializedAsync()
        {
            _loader.ShowLoader();
            try
            {
                if (!NavStateService.IsNavVisible)
                {
                    _navigationManager.NavigateTo("/", true);
                    _toastservice.ShowWarning("You are not authorized for this page!!!");
                    return;
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
        public async Task Save()
        {
            try
            {
                if (string.IsNullOrEmpty(uploadedImageUrl))
                {
                    _toastservice.ShowWarning("Please Upload a Image");
                    return;
                }
                else if (string.IsNullOrEmpty(memberSetupModel.FirstName))
                {
                    _toastservice.ShowWarning("Photo Name is required");
                    return;
                }
                else if (memberSetupModel.Gender == 0)
                {
                    _toastservice.ShowWarning("Gender is required");
                    return;
                }
                else if (memberSetupModel.MatrialStatus == 0)
                {
                    _toastservice.ShowWarning("Matrial Status is required");
                    return;
                }
                else if (memberSetupModel.Occupation == 0)
                {
                    _toastservice.ShowWarning("Occupation is required");
                    return;
                }
                else if (string.IsNullOrEmpty(memberSetupModel.Address))
                {
                    _toastservice.ShowWarning("Address is required");
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
                        title: "Are you sure you want to save this member?",
                        message1: "This will save the record. Do you want to proceed?",
                        options);
                    if (confirmation)
                    {
                        var response = await _familyTreeMemberManager.CreateFamilyTreeMember(memberSetupModel);
                        if (response.Succeeded)
                        {
                            _toastservice.ShowSuccess(response.Messages);
                            memberSetupModel = new();
                            uploadedImageUrl = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _toastservice.ShowWarning(ex.Message);
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
                memberSetupModel.imageUpload = new ImageUpload { FileByte = buffer, FileName = filename, Extension = extension };
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
            memberSetupModel.Age=await CalculateAgeFromDateOfBirth(memberSetupModel.DOB);
        }
    }
}