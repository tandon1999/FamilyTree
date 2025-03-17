using FamilyTreeApi.RequestModel;
using FamilyTreeApi.ResponseModel;
using FamilyTreeApi.Shared;

namespace FamilyTreeApi.Service.Interface
{
    public interface IBlogsService : IService
    {
        Task<IResponse> CreateBlogsPost(BlogsRequestModel model);
        Task<IResponse<List<BlogsResponseModel>>> GetAllBlogsPost();
        Task<IResponse> DeleteBlogsPost(int Id);
        Task<IResponse<BlogsRequestModel>> GetBlogsPostById(int Id);
    }
}
