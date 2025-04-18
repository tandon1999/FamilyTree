using AutoMapper.Execution;
using Blazored.Toast.Services;
using FamilyTree_UI.Shared.Services;
using FamilyTree_UI.ViewModels;
using FamilyTreeUI.Manager.Implementation;
using FamilyTreeUI.Manager.Interface;
using FamilyTreeUI.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Reflection.Metadata;

namespace FamilyTree_UI.Pages.FamilySetups
{
    public partial class FamilyTreePage
    {
        
        [Inject] public IFamilyTreeMemberManager _familyTreeMemberManager { get; set; } = default!;
        public FamilyTreevmodel familyTreeMembervmodel { get; set; } = new();

        public List<FamilyTreevmodel> familyTreeMemberlist { get; set; } = new();
        public string Imagesrc { get; set; }
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
        /*        public async Task GetAllFamilyDetails(int Id)
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

                            familyTreeMemberlist.RemoveAll(m => m.FatherId == Id || m.MotherId == Id || m.WifeId == Id || m.GenerationId  > familyTreeMemberlist.FirstOrDefault(x => x.Id == Id).GenerationId);

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
                    StateHasChanged();
                }*/
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

                    // Remove existing members related to the parent ID and higher generations
                    familyTreeMemberlist.RemoveAll(m => m.FatherId == Id || m.MotherId == Id || m.WifeId == Id || m.GenerationId > familyTreeMemberlist.FirstOrDefault(x => x.Id == Id).GenerationId);

                    // Remove members with the same GenerationId and WifeId to avoid duplicates, only if list contains more than 1 item.
                    var generationIdOfParent = familyTreeMemberlist.FirstOrDefault(x => x.Id == Id)?.GenerationId ?? 0;
                    if (response.Data.Count > 1)
                    {
                        familyTreeMemberlist.RemoveAll(m => m.GenerationId == generationIdOfParent && m.WifeId != null && response.Data.Any(r => r.Id != m.Id && r.WifeId == m.WifeId));
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
            StateHasChanged();
        }
        private async Task OnBranchIconClick(int memberId)
        {
            var member = familyTreeMemberlist.FirstOrDefault(m => m.Id == memberId);
            if (member != null)
            {
                await GetAllFamilyDetails(memberId);
            }
            StateHasChanged();
        }

        private string GetGenerationColor(int generationId)
        {
            switch (generationId % 15)
            {
                case 0: return "background-color: #f0f8ff;"; // AliceBlue
                case 1: return "background-color: #f5f5dc;"; // Beige
                case 2: return "background-color: #f0fff0;"; // Honeydew
                case 3: return "background-color: #f8f8ff;"; // GhostWhite
                case 4: return "background-color: #fff0f5;"; // LavenderBlush
                case 5: return "background-color: #ffe4e1;"; // MistyRose
                case 6: return "background-color: #f5f5f5;"; // WhiteSmoke
                case 7: return "background-color: #fafad2;"; // LightGoldenRodYellow
                case 8: return "background-color: #e6e6fa;"; // Lavender
                case 9: return "background-color: #fdf5e6;"; // OldLace
                case 10: return "background-color: #ffebcd;"; // BlanchedAlmond
                case 11: return "background-color: #faf0e6;"; // Linen
                case 12: return "background-color: #fffff0;"; // Ivory
                case 13: return "background-color: #f0e68c;"; // Khaki
                case 14: return "background-color: #ffffff;"; // LightGrey
                default: return "background-color: #d3d3d3;"; // White
            }
        }
        private string GetGenerationType(int generationId)
        {
            var generationMember = familyTreeMemberlist?.FirstOrDefault(m => m.GenerationId == generationId);
            return generationMember?.GenerationType ?? "Unknown";
        }
        private string GetMemberAlignment(int identificationId)
        {
            return identificationId switch
            {
                3 => "member-left",  // Son
                4 => "member-right", // Daughter
                _ => string.Empty,   // Default
            };
        }
        private string GetMemberColor(int identificationId)
        {
            return identificationId switch
            {
                1 => "#ADD8E6", // Light blue for sons
                3 => "#ADD8E6", // Light blue for sons
                4 => "#FFB6C1", // Light pink for daughters
                _ => "#FFFFFF"  // Default white background for others
            };
        }

    }
}