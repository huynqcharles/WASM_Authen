using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WASM_Authen.API.IServices;
using WASM_Authen.Shared.Models;

namespace WASM_Authen.API.Services
{
    public class UsersService : IUsersService
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UsersService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IEnumerable<ApplicationUser>> GetUsers()
        {
            return await this.userManager.Users.ToListAsync();
        }
    }
}
