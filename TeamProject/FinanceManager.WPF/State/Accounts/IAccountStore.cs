using FinanceManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.WPF.State.Accounts
{
    /// <summary>
    /// Store the state of the account.
    /// </summary>
    public interface IAccountStore
    {
        Account CurrentAccount { get; set; }
        event Action StateChanged;
    }
}
