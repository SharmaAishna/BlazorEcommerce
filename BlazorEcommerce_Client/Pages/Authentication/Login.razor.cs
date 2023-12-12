﻿using BlazorEcommerce_Client.Service.IService;
using EcommerceModel;
using Microsoft.AspNetCore.Components;

namespace BlazorEcommerce_Client.Pages.Authentication
{
    public partial class Login
    {
        private SignInRequestDTO signInRequestDTO = new();
        public bool IsProcessing { get; set; } = false;
        public bool ShowSignInErrors { get; set; }
        public string Errors { get; set; }

        [Inject]
        public IAuthenticationService _authService { get; set; }

        [Inject]
        public NavigationManager _navigationManager { get; set; }

        private async Task LogInUser()
        {
            ShowSignInErrors = false;
            IsProcessing = true;
            var result = await _authService.Login(signInRequestDTO);
            if (result.IsAuthSuccessful)
            {
                //Registeration is successful
                _navigationManager.NavigateTo("/");

            }
            else
            {
                //failure
                Errors = result.ErrorMessage;
                ShowSignInErrors = true;
            }
            IsProcessing = false;
        }
    }
}

