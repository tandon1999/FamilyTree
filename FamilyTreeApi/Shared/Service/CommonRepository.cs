using FamilyTreeApi.Shared.DataBaseAccess.Dapper.Implementation;
using FamilyTreeApi.Shared.DataBaseAccess.GenericRepository.Interface;
using FamilyTreeApi.Shared.Model;
using System.Data;

namespace FamilyTreeApi.Shared.Service
{
    public class CommonRepository : DapperRepository, ICommonRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IGenericRepository _genericRepository;

        public CommonRepository(ILogger<DapperRepository> logger, IConfiguration configuration, IGenericRepository genericRepository)
            : base(logger, configuration) // Pass both logger and configuration to the base class
        {
            _configuration = configuration;
            _genericRepository = genericRepository;
        }
        public async Task<List<DropDownModel>> GetDropDownAsync(string DDL_TYPE, string Filter1, string Filter2)
        {
            var ddl = await GetQueryResultAsync<DropDownModel>(
                "spDropDown",
                new { ddlType = DDL_TYPE, Filter1, Filter2 },
                CommandType.StoredProcedure
            );
            return ddl;
        }

    }
}
