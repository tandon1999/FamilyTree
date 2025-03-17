using FamilyTree_UI.Configuration.Extension;
using FamilyTree_UI.ViewModels;
using FamilyTreeUI.Manager.EndPoints;
using FamilyTreeUI.Manager.Interface;
using FamilyTreeUI.Models;
using FamilyTreeUI.Pages.Shared;
using FamilyTreeUI.ViewModels;

namespace FamilyTreeUI.Manager.Implementation
{
    public class FamilyTreeMemberManager : IFamilyTreeMemberManager
    {
        readonly HttpClient _httpClient = new();
        readonly private IHttpClientFactory _httpClientFactory = default!;
        public FamilyTreeMemberManager(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient("ApiGateway");
        }
        public async Task<IResponse> CreateFamilyTreeMember(FamilyMemberSetupModel model)
        {
                var response = await _httpClient.PostAsJsonAsync(FamilyTreeMemberEndPoints.CreateFamilyTreeMember, model);
                var data = await response.ToResult<IResponse>();
                return data;
        }

        public async Task<IResponse> DeleteFamilyTreeMember(int Id)
        {
            var response = await _httpClient.DeleteAsync(FamilyTreeMemberEndPoints.DeleteFamilyTreeMember(Id));
            var data = await response.ToResult<IResponse>();
            return data;
        }
        public async Task<FamilyMemberSetupModel> GetFamilyTreeMemberByid(int Id)
        {
            var response = await _httpClient.GetAsync(FamilyTreeMemberEndPoints.GetFamilyTreeMemberByid(Id));
            var data = await response.ToResult<FamilyMemberSetupModel>();
            return data.Data;
        }

        public async Task<IResponse<List<FamilyTreeMemberVModel>>> GetFamilyTreeMembers()
        {
            var response = await _httpClient.GetAsync(FamilyTreeMemberEndPoints.GetFamilyTreeMembers);
            var data = await response.ToResult<List<FamilyTreeMemberVModel>>();
            return data;
        }

        public async Task<IResponse<List<TimeLineViewModels>>> GetFamilyMemberTimeLine()
        {
           var response = await _httpClient.GetAsync(FamilyTreeMemberEndPoints.GetFamilyMemberTimeLine);
            var data = await response.ToResult<List<TimeLineViewModels>>();
            return data;
        }

        public async Task<IResponse<List<FamilyTreevmodel>>> FamilyDetailsByParentId(int Id)
        {
            var response = await _httpClient.GetAsync(FamilyTreeMemberEndPoints.FamilyDetailsByParentId(Id));
            var data = await response.ToResult<List<FamilyTreevmodel>>(); 
            return data;
        }
    }
}
