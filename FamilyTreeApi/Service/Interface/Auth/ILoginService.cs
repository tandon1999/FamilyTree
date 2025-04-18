using FamilyTreeApi.RequestModel.Auth;
using FamilyTreeApi.ResponseModel;
using FamilyTreeApi.Shared;

namespace FamilyTreeApi.Service.Interface.Auth
{
    public interface ILoginService : IService
    {
        Task<IResponse<LoginResponseModel>> CreateToken(LoginRequestModel request);
    }
}
