using FamilyTreeApi.Shared.Model;

namespace FamilyTreeApi.Shared.Service
{
    public interface ICommonService : IService
    {
        Task<List<DropDownModel>> GetDropDownAsync(string DDL_TYPE, string? Filter1 = null, string? Filter2 = null);
        Task<IResponse<List<DropDownModelApi>>> GetDropDownListAsync(string DropDownType, string? Filter1 = null, string? Filter2 = null);
    }
}
