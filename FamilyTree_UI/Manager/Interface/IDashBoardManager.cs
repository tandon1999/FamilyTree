using FamilyTree_UI.Shared;
using FamilyTree_UI.ViewModels;
using FamilyTreeUI.Pages.Shared;

namespace FamilyTree_UI.Manager.Interface
{
    public interface IDashBoardManager : IManager
    {
        Task<DashBoardViewModel> GetDashboardData();
        Task<IResponse<List<UpcommingAnniVModel>>> GetUpcommingAnniversary();
        Task<IResponse<List<LastestBlogPostVModel>>> GetLatestBlogsPost();
        Task<HeadersVModel> GetHeadres();
    }
}
