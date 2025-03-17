using FamilyTreeApi.RequestModel;
using FamilyTreeApi.ResponseModel;
using FamilyTreeApi.Shared;

namespace FamilyTreeApi.Service.Interface
{
    public interface ISetupService : IService
    {
        Task<IResponse> CreateGallerySetup(GallerySetupRequestModel model);
        Task<IResponse<List<GallerySetupResponseModel>>> GetAllGalleries();
        Task<IResponse> DeleteGalleryImage(int Id);
        Task<IResponse<GallerySetupRequestModel>> GetGalleryImageById(int Id);


        Task<IResponse<HistorySetupResponseModel>> GetHistoryDetails();
    }
}
