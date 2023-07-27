using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WASM_Authen.API.IServices;
using WASM_Authen.Shared.Models;
using WASM_Authen.Shared.ViewModels;

namespace WASM_Authen.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IRegisterService registerService;

        public RegisterController(IRegisterService registerService)
        {
            this.registerService = registerService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerUser, string role)
        {
            var result = await this.registerService.Register(registerUser, role);
            return StatusCode(result.StatusCode, result.Message);
        }
    }
}
