using AutoMapper;
using FamilyTreeApi.Param;
using FamilyTreeApi.RequestModel;
using FamilyTreeApi.ResponseModel;
using FamilyTreeApi.Service.Interface;
using FamilyTreeApi.Shared;
using FamilyTreeApi.Shared.DataBaseAccess.GenericRepository.Interface;
using Shared.Services.Interface;

namespace FamilyTreeApi.Service.Implementation
{
    public class FamilyMemberService : IFamilyMemberService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IMapper _mapper;
        private readonly IFileUploadService _fileuploadservice;

        public FamilyMemberService(IGenericRepository genericRepository, IMapper mapper, IFileUploadService fileUploadService)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
            _fileuploadservice = fileUploadService;
        }
        private readonly string StoreProc = "SpFamilyTreeSetup";
        public async Task<IResponse> CreateFamilyTreeMember(FamilyTreeMemberRequestModel model)
        {
            try
            {
                if (model.imageUpload != null)
                {
                    var newFolderName = "TandanImage";
                    var newFolderPath = Path.Combine("D:\\", newFolderName);
                    var filedata = await _fileuploadservice.UploadFileAsync(model.imageUpload.FileByte, model.imageUpload.FileName, newFolderPath);
                    model.ImagePath = filedata.ToString();
                }
                else
                {
                
                }
                FamilyTreeParam param = new();
                param = _mapper.Map<FamilyTreeParam>(model);
                param.Flag = 'C';
                var response = await _genericRepository.GetAsync<Response>(StoreProc, param);
                return await Response.SuccessAsync(response.Messages);
            }
            catch (Exception ex)
            {
                return await Response.FailAsync(ex.Message);
            }
        }

        public async Task<IResponse> DeleteFamilyTreeMember(int Id)
        {
            try
            {
                FamilyTreeParam param = new();
                param.Flag = 'D';
                param.Id = Id;
                var response = await _genericRepository.GetAsync<Response>(StoreProc, param);
                return await Response.SuccessAsync(response.Messages);
            }
            catch (Exception ex)
            {
                return await Response.FailAsync(ex.Message);
            }
        }

        public async Task<IResponse<FamilyTreeMemberRequestModel>> GetFamilyTreeMemberByid(int Id)
        {
            try
            {
                FamilyTreeParam param = new();
                param.Flag = 'I';
                param.Id = Id;
                var response = await _genericRepository.GetAsync<FamilyTreeMemberRequestModel>(StoreProc, param);
                if (response != null)
                {
                    if (response.ImagePath != null)
                    {
                        if ((System.IO.File.Exists(response.ImagePath)))
                            response.ImageByte = System.IO.File.ReadAllBytes(response.ImagePath);
                    }
                }
                return await Response<FamilyTreeMemberRequestModel>.SuccessAsync(response);
            }
            catch (Exception ex)
            {
                return await Response<FamilyTreeMemberRequestModel>.FailAsync(ex.Message);
            }
        }

        public async Task<IResponse<List<FamilyTreeMemberResponseModel>>> GetFamilyTreeMembers()
        {
            try
            {
                FamilyTreeParam param = new();
                param.Flag = 'G';
                var response = await _genericRepository.GetAllAsync<FamilyTreeMemberResponseModel>(StoreProc, param);
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

                return await Response<List<FamilyTreeMemberResponseModel>>.SuccessAsync(response.ToList());
            }
            catch (Exception ex)
            {
                return await Response<List<FamilyTreeMemberResponseModel>>.FailAsync(ex.Message);
            }
        }

        public async Task<IResponse<List<TimelineResponseModel>>> GetFamilyMemberTimeline()
        {
            try
            {
                FamilyTreeParam param = new();
                param.Flag = 'T';
                var response = await _genericRepository.GetAllAsync<TimelineResponseModel>(StoreProc, param);

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

                return await Response<List<TimelineResponseModel>>.SuccessAsync(response.ToList());
            }
            catch (Exception ex)
            {
                return await Response<List<TimelineResponseModel>>.FailAsync(ex.Message);
            }
        }

        public async Task<IResponse<List<FamilyTreeResponseModel>>> FamilyDetailsByParentId(int Id)
        {
            try
            {
                FamilyTreeParam param = new();
                param.Flag = 'U';
                param.Id = Id;
                var response = await _genericRepository.GetAllAsync<FamilyTreeResponseModel>(StoreProc, param);
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

                return await Response<List<FamilyTreeResponseModel>>.SuccessAsync(response.ToList());
            }
            catch (Exception ex)
            {
                return await Response<List<FamilyTreeResponseModel>>.FailAsync(ex.Message);
            }
        }
    }
}
