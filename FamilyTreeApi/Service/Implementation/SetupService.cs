using AutoMapper;
using FamilyTreeApi.Param;
using FamilyTreeApi.RequestModel;
using FamilyTreeApi.ResponseModel;
using FamilyTreeApi.Service.Interface;
using FamilyTreeApi.Shared;
using FamilyTreeApi.Shared.DataBaseAccess.GenericRepository.Implementation;
using FamilyTreeApi.Shared.DataBaseAccess.GenericRepository.Interface;
using Shared.Services.Interface;
using System.Text.Json;

namespace FamilyTreeApi.Service.Implementation
{
    public class SetupService : ISetupService
    {
        public IGenericRepository _genericrepo;
        public IMapper _mapper;
        public IFileUploadService _fileuploadservice;
        public SetupService(IGenericRepository genericRepository, IMapper mapper, IFileUploadService fileUploadService)
        {
            _genericrepo = genericRepository;
            _mapper = mapper;
            _fileuploadservice = fileUploadService;
        }
        public string sp1 = "spGallerySetup";
        public string sp2 = "spHistory";
        public async Task<IResponse> CreateGallerySetup(GallerySetupRequestModel model)
        {
            try
            {
                foreach (var item in model.gallerysetupList)
                {
                    if (item.imageUpload != null)
                    {
                        var newFolderName = "GalleryImage";
                        var newFolderPath = Path.Combine("D:\\", newFolderName);
                        var filedata = await _fileuploadservice.UploadFileAsync(item.imageUpload.FileByte, item.imageUpload.FileName, newFolderPath);
                        item.ImagePath = filedata.ToString();
                    }
                    else
                    {

                    }
                }

                model.allimagedetails = JsonSerializer.Serialize(model.gallerysetupList);
                GallerySetupParam param = new();
                param = _mapper.Map<GallerySetupParam>(model);
                param.Flag = 'C';
                var response = await _genericrepo.GetAsync<Response>(sp1, param);
                return await Response.SuccessAsync(response.Messages);
            }
            catch (Exception ex)
            {
                return await Response.FailAsync(ex.Message);
            }
        }

        public async Task<IResponse<HistorySetupResponseModel>> GetHistoryDetails()
        {
            try
            {
                HistorySetupParam param = new();
                param.Flag = 'H';
                var response = await _genericrepo.GetAsync<HistorySetupResponseModel>(sp2, param);
                return await Response<HistorySetupResponseModel>.SuccessAsync(response);
            }
            catch (Exception ex)
            {
                return await Response<HistorySetupResponseModel>.FailAsync(ex.Message);
            }
        }

        public async Task<IResponse<List<GallerySetupResponseModel>>> GetAllGalleries()
        {
            try
            {
                HistorySetupParam param = new();
                param.Flag = 'G';
                var response = await _genericrepo.GetAllAsync<GallerySetupResponseModel>(sp1, param);
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

                return await Response<List<GallerySetupResponseModel>>.SuccessAsync(response.ToList());
            }
            catch (Exception ex)
            {
                return await Response<List<GallerySetupResponseModel>>.FailAsync(ex.Message);
            }
        }

        public async Task<IResponse> DeleteGalleryImage(int Id)
        {
            try
            {
                HistorySetupParam param = new();
                param.Flag = 'D';
                param.Id = Id;
                var response = await _genericrepo.GetAsync<Response>(sp1, param);
                return await Response.SuccessAsync(response.Messages);
            }
            catch (Exception ex)
            {
                return await Response.FailAsync(ex.Message);
            }
        }

        public async Task<IResponse<GallerySetupRequestModel>> GetGalleryImageById(int Id)
        {
            try
            {
                HistorySetupParam param = new();
                param.Flag = 'I';
                param.Id = Id;
                var response = await _genericrepo.GetAsync<GallerySetupRequestModel>(sp1, param);
                if (response.ImagePath != null)
                {
                    if ((System.IO.File.Exists(response.ImagePath)))
                        response.ImageByte = System.IO.File.ReadAllBytes(response.ImagePath);
                }
                return await Response<GallerySetupRequestModel>.SuccessAsync(response);
            }
            catch (Exception ex)
            {
                return await Response<GallerySetupRequestModel>.FailAsync(ex.Message);
            }
        }
    }
}
