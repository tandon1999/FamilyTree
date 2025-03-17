using FamilyTreeApi.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FamilyTreeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {
        public IDashBoardService _dashboardservice;
        public DashBoardController(IDashBoardService dashboardservice)
        {
            _dashboardservice = dashboardservice;
        }

        [HttpGet("GetDashboardData")]
        public async Task<IActionResult> GetDashboardData()
        {
            var response = await _dashboardservice.GetDashBoardData();
            return Ok(response);
        }

        [HttpGet("GetUpcommingAnniversary")]
        public async Task<IActionResult> GetUpcommingAnniversary()
        {
            var response = await _dashboardservice.GetUpcommingAnniversary();
            return Ok(response);
        }

        [HttpGet("GetLatestBlogsPost")]
        public async Task<IActionResult> GetLatestBlogsPost()
        {
            var response = await _dashboardservice.GetLatestBlogsPost();
            return Ok(response);
        }

        [HttpGet("GetHeadres")]
        public async Task<IActionResult> GetHeadres()
        {
            var response = await _dashboardservice.GetHeadres();
            return Ok(response);
        }
    }
}
