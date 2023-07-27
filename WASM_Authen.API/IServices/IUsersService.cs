using WASM_Authen.Shared.Models;

namespace WASM_Authen.API.IServices
{
    public interface IUsersService
    {
        Task<IEnumerable<ApplicationUser>> GetUsers();
    }
}
