using FamilyTree_UI.Configuration.Extension;
using FamilyTree_UI.Manager.EndPoints;
using FamilyTree_UI.Manager.Interface;
using FamilyTree_UI.ViewModels;
using FamilyTreeUI.Pages.Shared;

namespace FamilyTree_UI.Manager.Implementation
{
    public class DashBoardManager : IDashBoardManager
    {
        readonly HttpClient _httpClient = new();
        readonly private IHttpClientFactory _httpClientFactory = default!;
        public DashBoardManager(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient("ApiGateway");
        }
        public async Task<DashBoardViewModel> GetDashboardData()
        {
            var response = await _httpClient.GetAsync(DashboardEndPoints.GetDashboardData);
            var data = await response.ToResult<DashBoardViewModel>();   
            return data.Data;
        }
        public async Task<HeadersVModel> GetHeadres()
        {
            var response = await _httpClient.GetAsync(DashboardEndPoints.GetHeadres);
            var data = await response.ToResult<HeadersVModel>();
            return data.Data;
        }
        public async Task<IResponse<List<LastestBlogPostVModel>>> GetLatestBlogsPost()
        {
            var res = await _httpClient.GetAsync(DashboardEndPoints.GetLatestBlogsPost);
            var data = await res.ToResult<List<LastestBlogPostVModel>>();
            return data;
        }
        public async Task<IResponse<List<UpcommingAnniVModel>>> GetUpcommingAnniversary()
        {
            var res = await _httpClient.GetAsync(DashboardEndPoints.GetUpcommingAnniversary);
            var data = await res.ToResult<List<UpcommingAnniVModel>>();
            return data;
        }
    }
}
