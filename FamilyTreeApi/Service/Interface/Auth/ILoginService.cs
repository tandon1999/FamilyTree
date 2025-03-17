using FamilyTreeApi.RequestModel.Auth;
using FamilyTreeApi.Shared;

namespace FamilyTreeApi.Service.Interface.Auth
{
    public interface ILoginService : IService
    {
        Task<IResponse> GetLoginDetails(LoginRequestModel request);
    }
}
