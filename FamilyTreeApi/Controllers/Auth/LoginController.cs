using FamilyTreeApi.RequestModel.Auth;
using FamilyTreeApi.Service.Interface.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FamilyTreeApi.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ILoginService _loginservice;
        public LoginController(ILoginService loginservice)
        {
            _loginservice = loginservice;
        }

        [HttpPost("GetLoginDetails")]
        public async Task<IActionResult> GetLoginDetails(LoginRequestModel model)
        {
            var response = await _loginservice.GetLoginDetails(model);
            return Ok(response);
        }
    }
}
