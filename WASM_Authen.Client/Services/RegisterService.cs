using System.Net.Http.Json;
using System.Text.Json;
using WASM_Authen.Client._Services;
using WASM_Authen.Shared.Models;
using WASM_Authen.Shared.ViewModels;

namespace WASM_Authen.Client.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly HttpClient httpClient;

        public RegisterService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<Response> Register(RegisterViewModel registerUser)
        {
            var role = "user";

            var result = await this.httpClient.PostAsJsonAsync($"api/register?role={role}", registerUser);

            var content = await result.Content.ReadAsStringAsync();

            var registerResponse = JsonSerializer.Deserialize<Response>(content,
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return registerResponse;
        }
    }
}
