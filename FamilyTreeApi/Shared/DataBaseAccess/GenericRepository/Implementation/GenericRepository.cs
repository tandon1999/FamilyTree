using FamilyTreeApi.Shared.CurrentUser;
using FamilyTreeApi.Shared.DataBaseAccess.Dapper.Interface;
using FamilyTreeApi.Shared.DataBaseAccess.GenericRepository.Interface;
using FamilyTreeApi.Shared.Model;
using System.Data;

namespace FamilyTreeApi.Shared.DataBaseAccess.GenericRepository.Implementation
{
    public class GenericRepository : IGenericRepository
    {
        private readonly IDapperRepository _dapperRepository;
        private readonly ICurrentUserService _currentUserService;

        public GenericRepository(IDapperRepository dapperRepository, ICurrentUserService currentUserService)
        {
            _dapperRepository = dapperRepository;
            _currentUserService = currentUserService;
        }

        public async Task<List<T>> GetAllAsync<T>(string spName, object obj, CommandType queryType)
            => await _dapperRepository.GetQueryResultAsync<T>(spName, obj, queryType);


        public async Task<T> GetAsync<T>(string spName, object obj, CommandType queryType)
            => await _dapperRepository.GetQueryFirstOrDefaultResultAsync<T>(spName, obj, queryType);

        public async Task<List<object>> GetFromMultipleQuery<T0, T1>(string sqlQuery, object sqlParam, CommandType queryType)
             => await _dapperRepository.GetFromMultipleQuery<T0, T1>(sqlQuery, sqlParam, queryType);

        public async Task<List<object>> GetFromMultipleQuery<T0, T1, T2>(string sqlQuery, object sqlParam, CommandType queryType)
            => await _dapperRepository.GetFromMultipleQuery<T0, T1, T2>(sqlQuery, sqlParam, queryType);

        public async Task<List<object>> GetFromMultipleQuery<T0, T1, T2, T3>(string sqlQuery, object sqlParam, CommandType queryType)
            => await _dapperRepository.GetFromMultipleQuery<T0, T1, T2, T3>(sqlQuery, sqlParam, queryType);

        public async Task<SystemResponse> UpdateAsync(string spName, object input, CommandType queryType)
        {
            if (input.GetType().GetProperty("CreatedBy") != null)
            {
                input.GetType().GetProperty("CreatedBy")!.SetValue(input, _currentUserService.UserId);
            }
            return await _dapperRepository.ExecuteAsync(spName, input, queryType);
        }

        public async Task<SystemResponse> InsertAsync(string spName, object input, CommandType queryType)
        {
            if (input.GetType().GetProperty("CreatedBy") != null)
            {
                input.GetType().GetProperty("CreatedBy")!.SetValue(input, _currentUserService.UserId);
            }
            return await _dapperRepository.ExecuteAsync(spName, input, queryType);
        }

        public async Task<SystemResponse> DeleteAsync(string spName, object input, CommandType queryType)
        {
            if (input.GetType().GetProperty("CreatedBy") != null)
            {
                input.GetType().GetProperty("CreatedBy")!.SetValue(input, _currentUserService.UserId);
            }
            return await _dapperRepository.ExecuteAsync(spName, input, queryType);
        }
    }

}
