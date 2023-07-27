using WASM_Authen.Shared.Models;
using WASM_Authen.Shared.ViewModels;

namespace WASM_Authen.API.IServices
{
    public interface IRegisterService
    {
        Task<Response> Register(RegisterViewModel registerUser, string role);
    }
}
