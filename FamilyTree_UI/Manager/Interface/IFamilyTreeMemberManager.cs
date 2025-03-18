using FamilyTree_UI.Shared;
using FamilyTree_UI.ViewModels;
using FamilyTreeUI.Models;
using FamilyTreeUI.Pages.Shared;
using FamilyTreeUI.ViewModels;

namespace FamilyTreeUI.Manager.Interface
{
    public interface IFamilyTreeMemberManager : IManager
    {
        Task<IResponse> CreateFamilyTreeMember(FamilyMemberSetupModel model);
        Task<IResponse> DeleteFamilyTreeMember(int Id);
        Task<IResponse<List<FamilyTreeMemberVModel>>> GetFamilyTreeMembers();
        Task<FamilyTreeMemberVModel> GetFamilyDetailsById(int Id);
        Task<IResponse<List<TimeLineViewModels>>> GetFamilyMemberTimeLine();
        Task<FamilyMemberSetupModel> GetFamilyTreeMemberByid(int Id);
        Task<IResponse<List<FamilyTreevmodel>>> FamilyDetailsByParentId(int Id);

    }
}
