using FinanceManager.WPF.Commands;
using FinanceManager.WPF.State.Authenticators;
using FinanceManager.WPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FinanceManager.WPF.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        // generic prop change that updates view model
        private string _username;
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        private string _password;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public MessageViewModel ErrorMessageViewModel { get; }
        public string ErrorMessage
        {
            set => ErrorMessageViewModel.Message = value;
        }

        public ICommand LoginCommand { get; }
        public ICommand ViewRegisterCommand { get; }


        public LoginViewModel(IAuthenticator authenticator, IRenavigator loginRenavigator, IRenavigator registerRenavigator)
        {
            ErrorMessageViewModel = new MessageViewModel();
            LoginCommand = new LoginCommand(this, authenticator, loginRenavigator);
            ViewRegisterCommand = new RenavigateCommand(registerRenavigator);
        }
    }
}
