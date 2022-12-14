using FinanceManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.WPF.State.Accounts
{
    public class AccountStore : IAccountStore
    {
        // allow people to see the global current account
        private Account _currentAccount;
        public Account CurrentAccount 
        {
            get
            {
                return _currentAccount;
            }
            set 
            {
                // tell people subscribed to us
                // that we have updated
                _currentAccount = value;
                StateChanged?.Invoke();
            }
        }
        public event Action StateChanged;
    }
}
