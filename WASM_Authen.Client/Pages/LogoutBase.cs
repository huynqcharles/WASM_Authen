using Microsoft.AspNetCore.Components;
using WASM_Authen.Client._Services;

namespace WASM_Authen.Client.Pages
{
    public class LogoutBase : ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public ILoginService LoginService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoginService.Logout();
            NavigationManager.NavigateTo("/");
        }
    }
}
