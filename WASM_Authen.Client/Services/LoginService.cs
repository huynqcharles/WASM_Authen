using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Data;
using System.Net.Http.Json;
using System.Text.Json;
using WASM_Authen.Client._Services;
using WASM_Authen.Client.Extensions;
using WASM_Authen.Shared.Models;
using WASM_Authen.Shared.ViewModels;

namespace WASM_Authen.Client.Services
{
    public class LoginService : ILoginService
    {
        private readonly HttpClient httpClient;
        private readonly ILocalStorageService localStorageService;
        private readonly AuthenticationStateProvider authenticationStateProvider;

        public LoginService(HttpClient httpClient, ILocalStorageService localStorageService,
            AuthenticationStateProvider authenticationStateProvider)
        {
            this.httpClient = httpClient;
            this.localStorageService = localStorageService;
            this.authenticationStateProvider = authenticationStateProvider;
        }
        public async Task<Response> Login(LoginViewModel loginUser)
        {
            var result = await this.httpClient.PostAsJsonAsync("api/login", loginUser);

            var content = await result.Content.ReadAsStringAsync();

            var loginResponse = JsonSerializer.Deserialize<Response>(content,
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            if (result.IsSuccessStatusCode)
            {
                await this.localStorageService.SetItemAsync("authToken", loginResponse.Token);
                ((CustomAuthenticationStateProvider)this.authenticationStateProvider).MarkUserAsAuthenticatedAsync(loginUser.UserName);
            }

            return loginResponse;
        }

        public async Task Logout()
        {
            await this.localStorageService.RemoveItemAsync("authToken");

            ((CustomAuthenticationStateProvider)this.authenticationStateProvider).MarkUserAsLoggedOut();
        }
    }
}
