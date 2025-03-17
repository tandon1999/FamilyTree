using FamilyTree_UI.Configuration.Extension;
using FamilyTree_UI.Manager.EndPoints;
using FamilyTree_UI.Manager.Interface;
using FamilyTree_UI.Models;
using FamilyTree_UI.ViewModels;
using FamilyTreeUI.Pages.Shared;

namespace FamilyTree_UI.Manager.Implementation
{
    public class BlogsManager : IBlogsManager
    {
        readonly HttpClient _httpClient = new();
        readonly private IHttpClientFactory _httpClientFactory = default!;
        public BlogsManager(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient("ApiGateway");
        }
        public async Task<IResponse> CreateBlogsPost(BlogsPostModel model)
        {
            var res = await _httpClient.PostAsJsonAsync(BlogsEndPoints.CreateBlogsPost, model);
            var data = await res.ToResult<IResponse>();
            return data;
        }

        public async Task<IResponse> DeleteBlogsPost(int Id)
        {
            var res = await _httpClient.DeleteAsync(BlogsEndPoints.DeleteBlogsPost(Id));
            var data = await res.ToResult<IResponse>();
            return data;
        }

        public async Task<IResponse<List<BlogsPostVModel>>> GetAllBlogsPost()
        {
            var res = await _httpClient.GetAsync(BlogsEndPoints.GetAllBlogsPost);
            var data = await res.ToResult<List<BlogsPostVModel>>();
            return data;
        }

        public async Task<BlogsPostModel> GetBlogsPostById(int Id)
        {
            var res = await _httpClient.GetAsync(BlogsEndPoints.GetBlogsPostById(Id));
            var data = await res.ToResult<BlogsPostModel>();
            return data.Data;
        }
    }
}
