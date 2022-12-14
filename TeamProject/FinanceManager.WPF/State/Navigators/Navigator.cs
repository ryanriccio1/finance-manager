using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FinanceManager.WPF.Commands;
using FinanceManager.WPF.ViewModels;
using FinanceManager.WPF.ViewModels.Factories;

namespace FinanceManager.WPF.State.Navigators
{
    class Navigator : INavigator
    {
        // allow people to subscribe to this property so they can
        // see when it changes
        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get { return _currentViewModel; } 
            set 
            {
                _currentViewModel = value;
                StateChanged?.Invoke();
            }
        }
        public event Action StateChanged;
    }
}
