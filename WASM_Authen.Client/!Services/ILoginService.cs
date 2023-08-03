using WASM_Authen.Shared.Models;
using WASM_Authen.Shared.ViewModels;

namespace WASM_Authen.Client._Services
{
    public interface ILoginService
    {
        Task<Response> Login(LoginViewModel loginUser);
        Task Logout();
    }
}
