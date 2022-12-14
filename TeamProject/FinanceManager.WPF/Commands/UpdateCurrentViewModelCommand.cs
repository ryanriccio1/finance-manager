using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FinanceManager.FinancialModelAPI.Services;
using FinanceManager.WPF.State.Navigators;
using FinanceManager.WPF.ViewModels;
using FinanceManager.WPF.ViewModels.Factories;

namespace FinanceManager.WPF.Commands
{
    public class UpdateCurrentViewModelCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly INavigator _navigator;
        private readonly IFinanceManagerViewModelFactory _viewModelFactory;

        public UpdateCurrentViewModelCommand(INavigator navigator, IFinanceManagerViewModelFactory viewModelFactory)
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;
        }

        public bool CanExecute(object parameter)
        {   // we are not async, so we should always be able to execute
            return true;
        }

        public void Execute(object parameter)
        {   // when we update the current view mode, we need to navigate to the new view 
            if (parameter is ViewType)
            {
                ViewType viewType = (ViewType)parameter;
                // get a view model from our view factory
                _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(viewType);
            }
        }
    }
}