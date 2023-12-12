using BlazorEcommerce_Client.Service.IService;
using EcommerceModel;
using Microsoft.AspNetCore.Components;

namespace BlazorEcommerce_Client.Pages.Authentication
{
    public partial class Register
    {
        private SignUpRequestDTO signUpRequestDTO = new();
        public bool IsProcessing { get; set; } = false;
        public bool ShowRegistrationErrors { get; set; }
        public IEnumerable<string> Errors { get; set; }
        
        [Inject]
        public IAuthenticationService _authService { get; set; }

        [Inject]
        public NavigationManager _navigationManager { get; set; }

        private async Task RegisterUser()
        {
            ShowRegistrationErrors = false;
            IsProcessing = true;
            var result = await _authService.RegisterUser(signUpRequestDTO);
            if (result.IsRegisterationSuccessful)
            {
                //Registeration is successful
                _navigationManager.NavigateTo("/login");

            }
            else
            {
                //failure
                Errors = result.Errors;
                ShowRegistrationErrors = true;
            }
            IsProcessing = false;
        }
    }
}
