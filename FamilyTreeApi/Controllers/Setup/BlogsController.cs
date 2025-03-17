using FamilyTreeApi.RequestModel;
using FamilyTreeApi.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FamilyTreeApi.Controllers.Setup
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        public IBlogsService _blogsservice;
        public BlogsController(IBlogsService blogsservice)
        {
            _blogsservice = blogsservice;
        }
        [HttpPost("CreateBlogsPost")]
        public async Task<IActionResult> CreateBlogsPost(BlogsRequestModel model)
        {
            var response = await _blogsservice.CreateBlogsPost(model);
            return Ok(response);
        }
        [HttpGet("GetAllBlogsPost")]
        public async Task<IActionResult> GetAllBlogsPost()
        {
            var response = await _blogsservice.GetAllBlogsPost();
            return Ok(response);
        }

        [HttpDelete("DeleteBlogsPost")]
        public async Task<IActionResult> DeleteBlogsPost(int Id)
        {
            var response = await _blogsservice.DeleteBlogsPost(Id);
            return Ok(response);
        }

        [HttpGet("GetBlogsPostById")]
        public async Task<IActionResult> GetBlogsPostById(int Id)
        {
            var response = await _blogsservice.GetBlogsPostById(Id);
            return Ok(response);
        }
    }
}
