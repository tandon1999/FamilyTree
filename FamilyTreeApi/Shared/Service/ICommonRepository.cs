using FamilyTreeApi.Shared.Model;

namespace FamilyTreeApi.Shared.Service
{
    public interface ICommonRepository
    {
        Task<List<DropDownModel>> GetDropDownAsync(string DDL_TYPE, string Filter1, string Filter2);
    }
}
