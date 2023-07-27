using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WASM_Authen.API.IServices;
using WASM_Authen.Shared.ViewModels;

namespace WASM_Authen.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService loginService;

        public LoginController(ILoginService loginService)
        {
            this.loginService = loginService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginUser)
        {
            var result = await this.loginService.Login(loginUser);

            if (result.IsSuccess)
            {
                return Ok(result.Token);
            }

            return StatusCode(result.StatusCode, result.Message);
        }
    }
}
