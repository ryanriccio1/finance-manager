using FinanceManager.Domain.Exceptions;
using FinanceManager.WPF.State.Authenticators;
using FinanceManager.WPF.State.Navigators;
using FinanceManager.WPF.ViewModels;
using SimpleTrader.WPF.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FinanceManager.WPF.Commands
{
    public class LoginCommand : AsyncCommandBase
    {
        private readonly LoginViewModel _loginViewModel;
        private readonly IAuthenticator _authenticator;
        private readonly IRenavigator _renavigator;

        public LoginCommand(LoginViewModel loginViewModel, IAuthenticator authenticator, IRenavigator renavigator)
        {
            _authenticator = authenticator;
            _renavigator = renavigator;
            _loginViewModel = loginViewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {   
            _loginViewModel.ErrorMessage = string.Empty;
            try
            {   // login using our authenticator and info from the view model
                await _authenticator.Login(_loginViewModel.Username, _loginViewModel.Password);
                _renavigator.Renavigate();
            }
            // handle exceptions
            catch (UserNotFoundException)
            {
                _loginViewModel.ErrorMessage = "Username does not exist.";
            }
            catch (InvalidPasswordException)
            {
                _loginViewModel.ErrorMessage = "Incorrect password.";
            }
            catch (Exception) 
            {
                _loginViewModel.ErrorMessage = "Login failed.";
            }
        }
    }
}
