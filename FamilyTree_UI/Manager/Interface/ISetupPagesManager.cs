using FamilyTree_UI.Models;
using FamilyTree_UI.Shared;
using FamilyTree_UI.ViewModels;
using FamilyTreeUI.Pages.Shared;

namespace FamilyTree_UI.Manager.Interface
{
    public interface ISetupPagesManager :IManager
    {
        Task<IResponse> CreateGallerySetup(GallerySetupModel model);
        Task<IResponse<List<GallerySetupVModel>>> GetAllGalleries();
        Task<IResponse> DeleteGalleryImage(int Id);
        Task<GallerySetupModel> GetGalleryImageById(int Id);


        Task<HistorySetupVModel> GetHistoryDetails();
    }
}
