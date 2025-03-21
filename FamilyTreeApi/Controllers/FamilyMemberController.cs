using FamilyTreeApi.RequestModel;
using FamilyTreeApi.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FamilyTreeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FamilyMemberController : ControllerBase
    {
        private readonly IFamilyMemberService _familyMemberService;
        public FamilyMemberController(IFamilyMemberService familyMemberService)
        {
            _familyMemberService = familyMemberService;
        }
        //Creating family setup
        [HttpPost("CreateFamilyMember")]
        public async Task<IActionResult> CreateFamilyMemberAsync(FamilyTreeMemberRequestModel model)
        {
            var response = await _familyMemberService.CreateFamilyTreeMember(model);
            return Ok(response);
        }

        //delete family setup
        [HttpDelete("DeleteFamilyMember")]
        public async Task<IActionResult> DeleteFamilyMemberAsync(int Id)
        {
            var response = await _familyMemberService.DeleteFamilyTreeMember(Id);
            return Ok(response);
        }

        //get all family details in list
        [HttpGet("GetAllFamilyMember")]
        public async Task<IActionResult> GetAllFamilyMemberAsync(int GenId)
        {
            var response = await _familyMemberService.GetFamilyTreeMembers(GenId);
            return Ok(response);
        }


        //get family details by id
        [HttpGet("GetFamilyMemberById")]
        public async Task<IActionResult> GetFamilyMemberByIdAsync(int Id)
        {
            var response = await _familyMemberService.GetFamilyTreeMemberByid(Id);
            return Ok(response);
        }

        //get timeline for family
        [HttpGet("GetFamilyMemberTimeLine")]
        public async Task<IActionResult> GetFamilyMemberTimeLine()
        {
            var response = await _familyMemberService.GetFamilyMemberTimeline();
            return Ok(response);
        }

        //get family details for the main UI
        [HttpGet("FamilyDetailsByParentId")]
        public async Task<IActionResult> FamilyDetailsByParentId(int Id)
        {
            var response = await _familyMemberService.FamilyDetailsByParentId(Id);
            return Ok(response);
        }
        
        //get family details by id
        [HttpGet("GetFamilyDetailsById")]
        public async Task<IActionResult> GetFamilyDetailsById(int Id)
        {
            var response = await _familyMemberService.GetFamilyDetailsById(Id);
            return Ok(response);
        }
    }
}
