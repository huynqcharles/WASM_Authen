using WASM_Authen.Shared.Models;
using WASM_Authen.Shared.ViewModels;

namespace WASM_Authen.API.IServices
{
    public interface ILoginService
    {
        Task<Response> Login(LoginViewModel loginUser);
    }
}
