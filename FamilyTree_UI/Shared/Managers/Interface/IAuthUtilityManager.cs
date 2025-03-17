using FamilyTree_UI.Shared.Models;

namespace FamilyTree_UI.Shared.Managers.Interface
{
    public interface IAuthUtilityManager : IManager
    {
        Task<List<DropDownListModel>> GetDropDownListAsync(string DropDownType, string? Filter1 = null, string? Filter2 = null, int AllorSelect = 0);
    }
}
