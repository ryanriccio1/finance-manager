using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.WPF.ViewModels
{
    public class MessageViewModel : ViewModelBase
    {
        // generic prop change that updates view model
        private string _message;
        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
                OnPropertyChanged(nameof(HasMessage));
            }
        }
        public bool HasMessage => !string.IsNullOrEmpty(Message);
    }
}
