using FinanceManager.WPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FinanceManager.WPF.Commands
{
    public class RenavigateCommand : ICommand
    {
        private readonly IRenavigator _renavigator;

        // this allows us to forcce navigate a user to a view/view model
        // we can bind this command to an action on a view
        public RenavigateCommand(IRenavigator renavigator)
        {
            _renavigator = renavigator;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {   // command our renavigator to renavigate
            _renavigator.Renavigate();
        }
    }
}
