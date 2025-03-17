using FamilyTree_UI.Configuration.Extension;
using FamilyTree_UI.Manager.EndPoints;
using FamilyTree_UI.Manager.Interface;
using FamilyTree_UI.Models;
using FamilyTree_UI.ViewModels;
using FamilyTreeUI.Pages.Shared;
using System.Collections.Generic;

namespace FamilyTree_UI.Manager.Implementation
{
    public class SetupPagesManager : ISetupPagesManager
    {
        readonly HttpClient _httpClient = new();
        readonly private IHttpClientFactory _httpClientFactory = default!;
        public SetupPagesManager(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient("ApiGateway");
        }
        public async Task<IResponse> CreateGallerySetup(GallerySetupModel model)
        {
            var res = await _httpClient.PostAsJsonAsync(SetupPagesEndPoints.CreateGallerySetup, model);
            var data = await res.ToResult<IResponse>();
            return data;
        }

        public async Task<IResponse> DeleteGalleryImage(int Id)
        {
            var res = await _httpClient.DeleteAsync(SetupPagesEndPoints.DeleteGalleryImage(Id));
            var data = await res.ToResult<IResponse>();
            return data;
        }

        public async Task<IResponse<List<GallerySetupVModel>>> GetAllGalleries()
        {
            var res = await _httpClient.GetAsync(SetupPagesEndPoints.GetAllGalleries);
            var data = await res.ToResult<List<GallerySetupVModel>>();
            return data;
        }

        public async Task<GallerySetupModel> GetGalleryImageById(int Id)
        {
            var res = await _httpClient.GetAsync(SetupPagesEndPoints.GetGalleryImageById(Id));
            var data = await res.ToResult<GallerySetupModel>();
            return data.Data;
        }

        public async Task<HistorySetupVModel> GetHistoryDetails()
        {
            var res = await _httpClient.GetAsync(SetupPagesEndPoints.GetHistoryDetails);
            var data = await res.ToResult<HistorySetupVModel>();
            return data.Data;
        }
    }
}
