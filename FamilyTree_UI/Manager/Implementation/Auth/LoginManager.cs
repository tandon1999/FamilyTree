using FamilyTree_UI.Configuration.Extension;
using FamilyTree_UI.Manager.EndPoints.Auth;
using FamilyTree_UI.Manager.Interface.Auth;
using FamilyTree_UI.Models.AuthModel;
using FamilyTreeUI.Pages.Shared;

namespace FamilyTree_UI.Manager.Implementation.Auth
{
    public class LoginManager : ILoginManager
    {
        readonly HttpClient _httpClient = new();
        readonly private IHttpClientFactory _httpClientFactory = default!;
        public LoginManager(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient("ApiGateway");
        }
        public async Task<IResponse> GetLoginDetails(LoginModel login)
        {
            var response = await _httpClient.PostAsJsonAsync(LoginEndPoints.GetLoginDetails, login);
            var data = await response.ToResult<IResponse>();
            return data;   
        }
    }
}
