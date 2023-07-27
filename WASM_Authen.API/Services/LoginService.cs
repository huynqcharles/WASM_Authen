using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WASM_Authen.API.IServices;
using WASM_Authen.Shared.Models;
using WASM_Authen.Shared.ViewModels;

namespace WASM_Authen.API.Services
{
    public class LoginService : ILoginService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;

        public LoginService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }
        public async Task<Response> Login(LoginViewModel loginUser)
        {
            // Check user cos ton tai khong
            var user = await this.userManager.FindByNameAsync(loginUser.UserName);
            if (user == null)
            {
                return new Response
                {
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "User nay khong ton tai!"
                };
            }
            else if (!await this.userManager.CheckPasswordAsync(user, loginUser.Password))
            {
                return new Response
                {
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Password khong hop le!"
                };
            }
            else
            {
                var roles = await this.userManager.GetRolesAsync(user);

                // Tao danh sach cac claims
                var claims = new List<Claim>()
                {
                    new Claim("Id", user.Id.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.UserName.ToString()),
                    new Claim(ClaimTypes.Email, user.Email.ToString()),
                    new Claim(ClaimTypes.Role, roles.FirstOrDefault())
                };

                // Tao JWT Token
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["JWT:SecretKey"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(this.configuration["JWT:Issuer"], this.configuration["JWT:Audience"],
                    claims, expires: DateTime.Now.AddDays(1), signingCredentials: signIn);

                return new Response
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Dang nhap thanh cong!",
                    Token = new JwtSecurityTokenHandler().WriteToken(token)
                };
            }
        }
    }
}
