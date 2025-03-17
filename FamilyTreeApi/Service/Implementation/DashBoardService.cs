using AutoMapper;
using FamilyTreeApi.Param;
using FamilyTreeApi.ResponseModel;
using FamilyTreeApi.Service.Interface;
using FamilyTreeApi.Shared;
using FamilyTreeApi.Shared.DataBaseAccess.GenericRepository.Interface;
using Shared.Services.Interface;

namespace FamilyTreeApi.Service.Implementation
{
    public class DashBoardService : IDashBoardService
    {
        private readonly IGenericRepository _genericRepository;

        private string StoreProc = "spDashboard";
        public DashBoardService(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public async Task<IResponse<DashBoardResponseModel>> GetDashBoardData()
        {
            try
            {
                DashbaordParam param = new();
                param.Flag = 'D';
                var response = await _genericRepository.GetAsync<DashBoardResponseModel>(StoreProc, param);
                return await Response<DashBoardResponseModel>.SuccessAsync(response);
            }
            catch (Exception ex)
            {
                return await Response<DashBoardResponseModel>.FailAsync(ex.Message);
            }
        }

        public async Task<IResponse<HeadersResponseModel>> GetHeadres()
        {
            try
            {
                DashbaordParam param = new();
                param.Flag = 'F';
                var response = await _genericRepository.GetAsync<HeadersResponseModel>(StoreProc, param);
                return await Response<HeadersResponseModel>.SuccessAsync(response);
            }
            catch (Exception ex)
            {
                return await Response<HeadersResponseModel>.FailAsync(ex.Message);
            }
        }

        public async Task<IResponse<List<LastestBlogPostResponseModel>>> GetLatestBlogsPost()
        {
            try
            {
                DashbaordParam param = new();
                param.Flag = 'B';
                var response = await _genericRepository.GetAllAsync<LastestBlogPostResponseModel>(StoreProc, param);
                foreach (var member in response)
                {
                    if (member.ImagePath != null)
                    {
                        if (System.IO.File.Exists(member.ImagePath))
                        {
                            member.ImageByte = System.IO.File.ReadAllBytes(member.ImagePath);
                        }
                    }
                }
                return await Response<List<LastestBlogPostResponseModel>>.SuccessAsync(response.ToList());
            }
            catch (Exception ex)
            {
                return await Response<List<LastestBlogPostResponseModel>>.FailAsync(ex.Message);
            }
        }

        public async Task<IResponse<List<UpcommingAnniResponseModel>>> GetUpcommingAnniversary()
        {
            try
            {
                DashbaordParam param = new();
                param.Flag = 'A';
                var response = await _genericRepository.GetAllAsync<UpcommingAnniResponseModel>(StoreProc, param);
                foreach (var member in response)
                {
                    if (member.ImagePath != null)
                    {
                        if (System.IO.File.Exists(member.ImagePath))
                        {
                            member.ImageByte = System.IO.File.ReadAllBytes(member.ImagePath);
                        }
                    }
                }
                return await Response<List<UpcommingAnniResponseModel>>.SuccessAsync(response.ToList());
            }
            catch (Exception ex)
            {
                return await Response<List<UpcommingAnniResponseModel>>.FailAsync(ex.Message);
            }
        }
    }
    public class DashbaordParam
    {
        public char Flag { get; set; }
    }
}
