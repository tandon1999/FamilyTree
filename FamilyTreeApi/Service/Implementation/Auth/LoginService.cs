using AutoMapper;
using FamilyTreeApi.Param.Auth;
using FamilyTreeApi.RequestModel.Auth;
using FamilyTreeApi.Service.Interface.Auth;
using FamilyTreeApi.Shared;
using FamilyTreeApi.Shared.DataBaseAccess.GenericRepository.Interface;

namespace FamilyTreeApi.Service.Implementation.Auth
{
    public class LoginService : ILoginService
    {
        private IGenericRepository _genericrepository;
        private IMapper _mapper;
        private string storeprocedure = "spLogin";
        public LoginService(IGenericRepository genericRepository,IMapper mapper)
        {
            _genericrepository = genericRepository;
            _mapper = mapper;
        }
        public async Task<IResponse> GetLoginDetails(LoginRequestModel request)
        {
            try
            {
                LoginParam param = new();
                param = _mapper.Map<LoginParam>(request);
                param.Flag = 'G';
                var response = await _genericrepository.GetAsync<Response>(storeprocedure, param);
                return await Response.SuccessAsync(response.Messages);
            }
            catch (Exception ex)
            {
                return await Response.FailAsync(ex.Message);
            }
        }
    }
}
