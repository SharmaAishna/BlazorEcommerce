using BlazorEcommerce_Client.Service.IService;
using Microsoft.AspNetCore.Components;
using System.Runtime.CompilerServices;

namespace BlazorEcommerce_Client.Pages.Authentication
{
    public partial class Logout
    {
        [Inject]
        public IAuthenticationService _authService { get; set; }

        [Inject]
        public NavigationManager _navigationManager { get; set; }
        protected async override Task OnInitializedAsync()
        {
            await _authService.Logout();
            _navigationManager.NavigateTo("/");
        }
    }
}
