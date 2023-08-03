using Microsoft.AspNetCore.Components;
using WASM_Authen.Client._Services;
using WASM_Authen.Shared.ViewModels;

namespace WASM_Authen.Client.Pages
{
    public class RegisterBase : ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IRegisterService RegisterService { get; set; }
        public RegisterViewModel RegisterViewModel { get; set; } = new RegisterViewModel();

        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }

        protected async Task HandleSubmitForm()
        {
            var result = await RegisterService.Register(RegisterViewModel);

            if (result.IsSuccess)
            {
                NavigationManager.NavigateTo("/");
            }
        }
    }
}
