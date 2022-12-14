using FinanceManager.WPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.WPF.ViewModels.Factories
{
    public interface IFinanceManagerViewModelFactory
    {
        ViewModelBase CreateViewModel(ViewType viewType);
    }
}
