using FamilyTreeApi.RequestModel;
using FamilyTreeApi.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FamilyTreeApi.Controllers.Setup
{
    [Route("api/[controller]")]
    [ApiController]
    public class SetupPagesController : ControllerBase
    {
        public ISetupService _setupservice;
        public SetupPagesController(ISetupService setupService)
        {
            _setupservice = setupService;
        }
        [HttpPost("CreateGallerySetup")]
        public async Task<IActionResult> CreateGallerySetup(GallerySetupRequestModel model)
        {
            var response = await _setupservice.CreateGallerySetup(model);
            return Ok(response);
        }
        [HttpGet("GetAllGalleries")]
        public async Task<IActionResult> GetAllGalleries()
        {
            var response = await _setupservice.GetAllGalleries();
            return Ok(response);
        }

        [HttpDelete("DeleteGalleyImage")]
        public async Task<IActionResult> DeleteGalleryImage(int Id)
        {
            var response = await _setupservice.DeleteGalleryImage(Id);
            return Ok(response);
        }
        
        [HttpGet("GetGalleryImageById")]
        public async Task<IActionResult> GetGalleryImageById(int Id)
        {
            var response = await _setupservice.GetGalleryImageById(Id);
            return Ok(response);
        }


        [HttpPost("GetHistoryDetails")]
        public async Task<IActionResult> GetHistoryDetails()
        {
            var response = await _setupservice.GetHistoryDetails();
            return Ok(response);
        }
    }
}
