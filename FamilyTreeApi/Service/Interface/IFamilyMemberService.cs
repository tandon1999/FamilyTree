using FamilyTreeApi.RequestModel;
using FamilyTreeApi.ResponseModel;
using FamilyTreeApi.Shared;

namespace FamilyTreeApi.Service.Interface
{
    public interface IFamilyMemberService :IService
    {
        Task<IResponse> CreateFamilyTreeMember(FamilyTreeMemberRequestModel model);
        Task<IResponse> DeleteFamilyTreeMember(int Id);
        Task<IResponse<List<FamilyTreeMemberResponseModel>>> GetFamilyTreeMembers();
        Task<IResponse<List<TimelineResponseModel>>> GetFamilyMemberTimeline();
        Task<IResponse<FamilyTreeMemberRequestModel>> GetFamilyTreeMemberByid(int Id);
        Task<IResponse<List<FamilyTreeResponseModel>>> FamilyDetailsByParentId(int Id);
    }
}
