using AutoMapper;
using FamilyTreeApi.Param;
using FamilyTreeApi.RequestModel;
using FamilyTreeApi.ResponseModel;
using FamilyTreeApi.Service.Interface;
using FamilyTreeApi.Shared;
using FamilyTreeApi.Shared.DataBaseAccess.GenericRepository.Interface;
using Shared.Services.Interface;
using System.Text.Json;

namespace FamilyTreeApi.Service.Implementation
{
    public class BlogsService : IBlogsService
    {
        public IGenericRepository _genericrepo;
        public IMapper _mapper;
        public IFileUploadService _fileuploadservice;
        public BlogsService(IGenericRepository genericRepository, IMapper mapper, IFileUploadService fileUploadService)
        {
            _genericrepo = genericRepository;
            _mapper = mapper;
            _fileuploadservice = fileUploadService;
        }
        public string storeProcedure = "spBlogs";
        public async Task<IResponse> CreateBlogsPost(BlogsRequestModel model)
        {
            try
            {
                if (model.imageUpload != null)
                {
                    var newFolderName = "Blogs";
                    var newFolderPath = Path.Combine("D:\\", newFolderName);
                    var filedata = await _fileuploadservice.UploadFileAsync(model.imageUpload.FileByte, model.imageUpload.FileName, newFolderPath);
                    model.ImagePath = filedata.ToString();
                }
                else
                {

                }
                BlogsParam param = new();
                param = _mapper.Map<BlogsParam>(model);
                param.Flag = 'C';
                var response = await _genericrepo.GetAsync<Response>(storeProcedure, param);
                return await Response.SuccessAsync(response.Messages);
            }
            catch (Exception ex)
            {
                return await Response.FailAsync(ex.Message);
            }
        }

        public async Task<IResponse<List<BlogsResponseModel>>> GetAllBlogsPost()
        {
            try
            {
                BlogsParam param = new();
                param.Flag = 'G';
                var response = await _genericrepo.GetAllAsync<BlogsResponseModel>(storeProcedure,param);
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
                return await Response<List<BlogsResponseModel>>.SuccessAsync(response.ToList());
            }
            catch (Exception ex)
            {
                return await Response<List<BlogsResponseModel>>.FailAsync(ex.Message);
            }
        }

        public async Task<IResponse> DeleteBlogsPost(int Id)
        {
            try
            {
                BlogsParam param = new();
                param.Flag = 'D';
                param.Id = Id;
                var response = await _genericrepo.GetAsync<Response>(storeProcedure, param);
                return await Response.SuccessAsync(response.Messages);
            }
            catch (Exception ex)
            {
                return await Response.FailAsync(ex.Message);
            }
        }

        public async Task<IResponse<BlogsRequestModel>> GetBlogsPostById(int Id)
        {
            try
            {
                BlogsParam param = new();
                param.Flag = 'I';
                param.Id = Id;
                var response = await _genericrepo.GetAsync<BlogsRequestModel>(storeProcedure, param);
                if (response.ImagePath != null)
                {
                    if ((System.IO.File.Exists(response.ImagePath)))
                        response.ImageByte = System.IO.File.ReadAllBytes(response.ImagePath);
                }
                return await Response<BlogsRequestModel>.SuccessAsync(response);
            }
            catch (Exception ex)
            {
                return await Response<BlogsRequestModel>.FailAsync(ex.Message);
            }
        }
    }
}
