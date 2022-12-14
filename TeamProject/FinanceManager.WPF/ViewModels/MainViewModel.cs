using FinanceManager.WPF.Commands;
using FinanceManager.WPF.State.Authenticators;
using FinanceManager.WPF.State.Navigators;
using FinanceManager.WPF.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FinanceManager.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IFinanceManagerViewModelFactory _viewModelFactory;
        private readonly INavigator _navigator;
        private readonly IAuthenticator _authenticator;

        public bool IsLoggedIn => _authenticator.IsLoggedIn;
        public ViewModelBase CurrentViewModel => _navigator.CurrentViewModel;

        public ICommand UpdateCurrentViewModelCommand { get; }
        public ICommand LogoutCommand { get; }

        public MainViewModel(INavigator navigator, IFinanceManagerViewModelFactory viewModelFactory, IAuthenticator authenticator, ViewModelDelegateRenavigator<LoginViewModel> renavigator) 
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;
            _authenticator = authenticator;

            _navigator.StateChanged += Navigator_StateChanged;
            _authenticator.StateChanged += Authenticator_StateChanged;

            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator, _viewModelFactory);
            LogoutCommand = new LogoutCommand(renavigator, authenticator);
            UpdateCurrentViewModelCommand.Execute(ViewType.Login);
        }

        private void Navigator_StateChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        private void Authenticator_StateChanged()
        {
            OnPropertyChanged(nameof(IsLoggedIn));
        }
    }
}
