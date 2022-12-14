using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FinanceManager.WPF.ViewModels;

namespace FinanceManager.WPF.State.Navigators
{
    public enum ViewType
    {
        Home,
        Portfolio,
        Buy,
        Sell,
        Login
    }

    public interface INavigator
    {
        public ViewModelBase CurrentViewModel { get; set; }
        event Action StateChanged;
    }
}
