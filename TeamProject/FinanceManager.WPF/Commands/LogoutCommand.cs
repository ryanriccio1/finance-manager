using FinanceManager.Domain.Exceptions;
using FinanceManager.WPF.State.Accounts;
using FinanceManager.WPF.State.Authenticators;
using FinanceManager.WPF.State.Navigators;
using FinanceManager.WPF.ViewModels;
using SimpleTrader.WPF.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.WPF.Commands
{
    public class LogoutCommand : AsyncCommandBase
    {
        private readonly IRenavigator _renavigator;
        private readonly IAuthenticator _authenticator;

        public LogoutCommand(IRenavigator renavigator, IAuthenticator authenticator)
        {
            _authenticator = authenticator;
            _renavigator = renavigator;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _authenticator.Logout();
            _renavigator.Renavigate();
        }
    }
}