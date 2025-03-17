using FamilyTreeApi.Shared.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FamilyTreeApi.Controllers.Shared
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilityController : ControllerBase
    {
        private readonly ICommonService _commanService;
        public UtilityController(ICommonService commanService)
        {
            _commanService = commanService;
        }
        [HttpGet("GetDropDownList")]
        public async Task<IActionResult> GetDropDownList(string DropDownType, string? Filter1 = null, string? Filter2 = null)
        {
            var data = await _commanService.GetDropDownListAsync(DropDownType, Filter1, Filter2);
            return Ok(data);
        }
    }
}
