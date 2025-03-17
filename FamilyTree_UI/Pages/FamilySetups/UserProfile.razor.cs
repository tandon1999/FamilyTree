
using Blazored.Toast.Services;
using FamilyTree_UI.Shared.Services;
using FamilyTreeUI.Manager.Interface;
using FamilyTreeUI.Models;
using FamilyTreeUI.ViewModels;
using Microsoft.AspNetCore.Components;

namespace FamilyTree_UI.Pages.FamilySetups
{
    public partial class UserProfile
    {
        [Inject] public NavStateService NavStateService { get; set; }
        [Inject] public IToastService _toastservice { get; set; } = default!;
        [Inject] public IFamilyTreeMemberManager _familyTreeMemberManager { get; set; } = default!;
        public FamilyMemberSetupModel memberSetupModel { get; set; } = new();
        public string uploadedImageUrl;
        [Parameter] public string Id { get; set; } = "0";
        protected override async Task OnInitializedAsync()
        {
            NavStateService.SetNavVisibility(true);
            if (!string.IsNullOrEmpty(Id))
            {
                await GetById(int.Parse(Id));
            }
        }
        public async Task GetById(int Id)
        {
            try
            {
                var response = await _familyTreeMemberManager.GetFamilyTreeMemberByid(Id);
                memberSetupModel = response;
                if (response != null)
                {
                    memberSetupModel.IsDeath = response.DeathDate.HasValue;
                    if (response.ImageByte != null)
                    {
                        uploadedImageUrl = "data:image/png;base64," + Convert.ToBase64String(response.ImageByte);
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

    }
}