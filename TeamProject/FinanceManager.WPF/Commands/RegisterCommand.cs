using FinanceManager.Domain.Services.AuthenticationServices;
using FinanceManager.WPF.State.Authenticators;
using FinanceManager.WPF.State.Navigators;
using FinanceManager.WPF.ViewModels;
using SimpleTrader.WPF.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.WPF.Commands
{
    public class RegisterCommand : AsyncCommandBase
    {
        private readonly RegisterViewModel _registerViewModel;
        private readonly IAuthenticator _authenticator;
        private readonly IRenavigator _registerRenavigator;

        public RegisterCommand(RegisterViewModel registerViewModel, IAuthenticator authenticator, IRenavigator registerRenavigator)
        {   // get the info from the view model, the authenticator, and the renavigator
            _registerViewModel = registerViewModel;
            _authenticator = authenticator;
            _registerRenavigator = registerRenavigator;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _registerViewModel.ErrorMessage = string.Empty;
            try
            {
                RegistrationResult registrationResult = await _authenticator.Register(
                       _registerViewModel.Email,
                       _registerViewModel.Username,
                       _registerViewModel.Password,
                       _registerViewModel.ConfirmPassword);

                // perform different actions based on the result of the registration
                switch (registrationResult)
                {
                    case RegistrationResult.Success:
                        _registerRenavigator.Renavigate();
                        break;
                    case RegistrationResult.PasswordsDoNotMatch:
                        _registerViewModel.ErrorMessage = "Passwords do not match.";
                        break;
                    case RegistrationResult.EmailAlreadyExits:
                        _registerViewModel.ErrorMessage = "Email already in use.";
                        break;
                    case RegistrationResult.UsernameAlreadyExists:
                        _registerViewModel.ErrorMessage = "Username already in use.";
                        break;
                    default:
                        _registerViewModel.ErrorMessage = "Registration failed.";
                        break;
                }
            }
            catch (Exception)
            {
                _registerViewModel.ErrorMessage = "Registration failed.";
            }
        }
    }
}