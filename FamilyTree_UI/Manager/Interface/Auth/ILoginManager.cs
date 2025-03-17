using FamilyTree_UI.Models.AuthModel;
using FamilyTree_UI.Shared;
using FamilyTreeUI.Pages.Shared;

namespace FamilyTree_UI.Manager.Interface.Auth
{
    public interface ILoginManager : IManager
    {
        Task<IResponse> GetLoginDetails(LoginModel login);
    }
}
