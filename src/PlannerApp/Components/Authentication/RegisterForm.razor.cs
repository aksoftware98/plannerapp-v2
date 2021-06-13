using Microsoft.AspNetCore.Components;
using PlaneerApp.Client.Services.Exceptions;
using PlaneerApp.Client.Services.Interfaces;
using PlannerApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerApp.Components
{
    public partial class RegisterForm
    {


        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        private RegisterRequest _model = new();
        private bool _isBusy = false;
        private string _errorMessage = string.Empty;

        private async Task RegisterUserAsync()
        {
            _isBusy = true;
            _errorMessage = string.Empty;

            try
            {
                await AuthenticationService.RegisterUserAsync(_model);
                Navigation.NavigateTo("/authentication/login");
            }
            catch (ApiException ex)
            {
                // Hanlde the errors of the aPI 
                // TODO: Log those errors 
                _errorMessage = ex.ApiErrorResponse.Message;
            }
            catch(Exception ex)
            {
                // Handle errors 
                _errorMessage = ex.Message;
            }

            _isBusy = false;
        }

        private void RedirectToLogin()
        {
            Navigation.NavigateTo("/authentication/login");
        }

    }
}
