using FamilyTree_UI.Configuration.Extension;
using FamilyTree_UI.Shared.Managers.Interface;
using FamilyTree_UI.Shared.Models;
using System.Reflection;

namespace FamilyTree_UI.Shared.Managers.Implementation
{
    public class AuthUtilityManager : IAuthUtilityManager
    {
        private readonly HttpClient _httpClient = new();
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ICacheService _cacheservice;

        public AuthUtilityManager(IHttpClientFactory httpClientFactory, ICacheService cacheservice)
        {
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient("ApiGateway");
            _cacheservice = cacheservice;
        }
        public async Task<List<DropDownListModel>> GetDropDownListAsync(string DropDownType, string? Filter1 = null, string? Filter2 = null, int AllorSelect = 0)
        {
            try
            {
                List<DropDownListModel> dropDownListModels = new();
                var cacheName = typeof(CacheConstants).GetField(DropDownType, BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
                var cacheNameData = cacheName == null ? null : (string)cacheName.GetValue(null);
                if (cacheNameData != null)
                {
                    dropDownListModels = await _cacheservice.GetData<List<DropDownListModel>>(cacheNameData);
                    if (dropDownListModels == null)
                    {
                        //  await SetHeader();
                        var Data = await _httpClient.GetAsync(AuthUtilityEndPoint.DropDownListFilter(DropDownType, Filter1, Filter2));
                        var list = await Data.ToResult<List<DropDownListModel>>();
                        if (list.Succeeded)
                            dropDownListModels = list.Data;
                        else
                            throw new ArgumentException(list.Messages);
                        await _cacheservice.SetData(cacheNameData, dropDownListModels, 1440);
                    }
                }
                else
                {
                    // await SetHeader();
                    var Data = await _httpClient.GetAsync(AuthUtilityEndPoint.DropDownListFilter(DropDownType, Filter1, Filter2));
                    var list = await Data.ToResult<List<DropDownListModel>>();
                    if (list.Succeeded)
                        dropDownListModels = list.Data;
                    else
                        throw new ArgumentException(list.Messages);
                }

                DropDownListModel dropDownListModel = new();
                if (AllorSelect == 1)
                {
                    dropDownListModel.Id = "-1";
                    dropDownListModel.Value = "All";
                    dropDownListModels.Insert(0, dropDownListModel);
                }
                else if (AllorSelect == 0)
                {
                    dropDownListModel.Id = "0";
                    dropDownListModel.Value = "Select";
                    dropDownListModels.Insert(0, dropDownListModel);
                }

                return dropDownListModels;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
