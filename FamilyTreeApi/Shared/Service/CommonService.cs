using AutoMapper;
using FamilyTreeApi.Shared.Model;

namespace FamilyTreeApi.Shared.Service
{
    public class CommonService : ICommonService
    {
        private readonly ICommonRepository _commonRepository;
        private readonly IMapper _mapper;
        public CommonService(ICommonRepository commonRepository, IMapper mapper)
        {
            _commonRepository = commonRepository;
            _mapper = mapper;
        }
        public async Task<List<DropDownModel>> GetDropDownAsync(string DDL_TYPE, string? Filter1 = null, string? Filter2 = null)
        {
            var ddl = await _commonRepository.GetDropDownAsync(DDL_TYPE, Filter1, Filter2);
            return ddl;
        }

        public async Task<IResponse<List<DropDownModelApi>>> GetDropDownListAsync(string DropDownType, string? Filter1 = null, string? Filter2 = null)
        {
            var ddl = await _commonRepository.GetDropDownAsync(DropDownType, Filter1, Filter2);
            var data = _mapper.Map<List<DropDownModel>, List<DropDownModelApi>>(ddl);
            return await Response<List<DropDownModelApi>>.SuccessAsync(data);
        }
    }
}
