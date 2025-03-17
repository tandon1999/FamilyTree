using FamilyTree_UI.Models;
using FamilyTree_UI.Shared;
using FamilyTree_UI.ViewModels;
using FamilyTreeUI.Pages.Shared;

namespace FamilyTree_UI.Manager.Interface
{
    public interface IBlogsManager : IManager
    {
        Task<IResponse> CreateBlogsPost(BlogsPostModel model);
        Task<IResponse<List<BlogsPostVModel>>> GetAllBlogsPost();
        Task<IResponse> DeleteBlogsPost(int Id);
        Task<BlogsPostModel> GetBlogsPostById(int Id);
    }
}
