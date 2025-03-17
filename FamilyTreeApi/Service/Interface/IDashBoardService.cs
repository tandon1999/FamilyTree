using FamilyTreeApi.ResponseModel;
using FamilyTreeApi.Shared;
using FamilyTreeApi.Shared.DataBaseAccess.GenericRepository.Interface;

namespace FamilyTreeApi.Service.Interface
{
    public interface IDashBoardService : IService
    {
        Task<IResponse<DashBoardResponseModel>> GetDashBoardData();
        Task<IResponse<List<UpcommingAnniResponseModel>>> GetUpcommingAnniversary();
        Task<IResponse<List<LastestBlogPostResponseModel>>> GetLatestBlogsPost();
        Task<IResponse<HeadersResponseModel>> GetHeadres();
    }
}
