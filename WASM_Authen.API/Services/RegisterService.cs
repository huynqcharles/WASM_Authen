using Microsoft.AspNetCore.Identity;
using WASM_Authen.API.IServices;
using WASM_Authen.Shared.Models;
using WASM_Authen.Shared.ViewModels;

namespace WASM_Authen.API.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public RegisterService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<Response> Register(RegisterViewModel registerUser, string role)
        {
            // Check user co ton tai khong
            if (await this.userManager.FindByEmailAsync(registerUser.Email) != null)
            {
                return new Response
                {
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Email nay da ton tai!"
                };
            }
            else if (await this.userManager.FindByEmailAsync(registerUser.UserName) != null)
            {
                return new Response
                {
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Username nay da ton tai!"
                };
            }
            else
            {
                // Create an identity user
                ApplicationUser applicationUser = new()
                {
                    UserName = registerUser.UserName,
                    Email = registerUser.Email,
                    FirstName = registerUser.FirstName,
                    LastName = registerUser.LastName
                };

                // Check role co ton tai khong
                if (await this.roleManager.RoleExistsAsync(role))
                {
                    // Them user vao database
                    var result = await this.userManager.CreateAsync(applicationUser, registerUser.Password);

                    // Check dang ky that bai
                    if (!result.Succeeded)
                    {
                        return new Response
                        {
                            IsSuccess = false,
                            StatusCode = StatusCodes.Status400BadRequest,
                            Message = "Dang ky that bai, co loi gi do xay ra!"
                        };
                    }

                    // Add role to user
                    await this.userManager.AddToRoleAsync(applicationUser, role);
                    return new Response
                    {
                        IsSuccess = true,
                        StatusCode = StatusCodes.Status201Created,
                        Message = "Dang ky thanh cong!"
                    };
                }
                else
                {
                    return new Response
                    {
                        IsSuccess = false,
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "Role khong ton tai!"
                    };
                }
            }
        }
    }
}
