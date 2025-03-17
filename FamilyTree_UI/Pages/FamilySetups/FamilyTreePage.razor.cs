using AutoMapper.Execution;
using Blazored.Toast.Services;
using FamilyTree_UI.Shared.Services;
using FamilyTree_UI.ViewModels;
using FamilyTreeUI.Manager.Implementation;
using FamilyTreeUI.Manager.Interface;
using FamilyTreeUI.ViewModels;
using Microsoft.AspNetCore.Components;

namespace FamilyTree_UI.Pages.FamilySetups
{
    public partial class FamilyTreePage
    {
        [Inject] public IToastService _toastservice { get; set; } = default!;
        [Inject] public IFamilyTreeMemberManager _familyTreeMemberManager { get; set; } = default!;
        public FamilyTreevmodel familyTreeMembervmodel { get; set; } = new();

        public List<FamilyTreevmodel> familyTreeMemberlist { get; set; } = new();
        public string Imagesrc { get; set; }
        [Inject] private LoaderService _loader { get; set; } = default!;
        protected override async Task OnInitializedAsync()
        {
            _loader.ShowLoader();
            try
            {
                await GetAllFamilyDetails(0);
                var firstMember = familyTreeMemberlist?.FirstOrDefault();
                if (firstMember != null)
                {
                    firstMember.IsIconDisabled = true;
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

        public async Task GetAllFamilyDetails(int Id)
        {
            try
            {
                var response = await _familyTreeMemberManager.FamilyDetailsByParentId(Id);
                if (response?.Data != null && response.Data.Count > 0)
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
                    if (familyTreeMemberlist == null)
                    {
                        familyTreeMemberlist = new List<FamilyTreevmodel>();
                    }
                    var existingMember = familyTreeMemberlist.FirstOrDefault(m => m.Id == Id);
                    if (existingMember != null)
                    {
                        if (response.Data.Count == 1)
                        {
                            existingMember.IsIconDisabled = false;
                        }
                    }
                    foreach (var member in response.Data)
                    {
                        if (!familyTreeMemberlist.Any(m => m.Id == member.Id))
                        {
                            familyTreeMemberlist.Add(member);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _toastservice.ShowWarning(ex.Message);
            }
        }

        private async Task OnBranchIconClick(int memberId)
        {
            var member = familyTreeMemberlist.FirstOrDefault(m => m.Id == memberId);
            if (member != null && !member.IsIconDisabled)
            {
                // Disable the icon immediately to prevent multiple clicks
                member.IsIconDisabled = true;

                // Fetch family details for this member
                await GetAllFamilyDetails(memberId);

                // Refresh the UI to reflect changes
                StateHasChanged();
            }
        }

        private bool ShouldDisplayBranchIcon(string sonIds, string daughterIds, int wifeId)
        {
            var sonIdList = !string.IsNullOrEmpty(sonIds) ? sonIds.Split(',').Where(id => id != "0").ToList() : new List<string>();
            var daughterIdList = !string.IsNullOrEmpty(daughterIds) ? daughterIds.Split(',').Where(id => id != "0").ToList() : new List<string>();

            return wifeId != 0 || sonIdList.Count > 0 || daughterIdList.Count > 0;
        }
    }
}