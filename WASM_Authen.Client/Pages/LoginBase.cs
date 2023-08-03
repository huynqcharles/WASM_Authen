using Microsoft.AspNetCore.Components;
using WASM_Authen.Client._Services;
using WASM_Authen.Shared.ViewModels;

namespace WASM_Authen.Client.Pages
{
    public class LoginBase : ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public ILoginService LoginService { get; set; }
        public LoginViewModel LoginViewModel { get; set; } = new LoginViewModel();

        public string ErrorMessage { get; set; }

        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }

        protected async Task HandleSubmitForm()
        {
            var result = await LoginService.Login(LoginViewModel);

            if (result.IsSuccess)
            {
                NavigationManager.NavigateTo("/");
            }
            else
            {
                ErrorMessage = result.Message;
            }
        }
    }
}
