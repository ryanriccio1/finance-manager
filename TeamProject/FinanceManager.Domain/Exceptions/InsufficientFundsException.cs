using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.Exceptions
{
    public class InsufficientFundsException : Exception
    {
        // the user's balance
        public double AccountBalance { get; set; }
        
        // the balance needed to buy this stock
        public double RequiredBalance { get; set; }

        /// <summary>
        /// Exception to handle when the user does not have enough money.
        /// </summary>
        /// <param name="accountBalance">User's account balance.</param>
        /// <param name="requiredBalance">The balance the account needs to buy these assets.</param>
        public InsufficientFundsException(double accountBalance, double requiredBalance)
        {
            AccountBalance = accountBalance;
            RequiredBalance = requiredBalance;
        }

        public InsufficientFundsException(double accountBalance, double requiredBalance, string? message) : base(message)
        {
            AccountBalance = accountBalance;
            RequiredBalance = requiredBalance;
        }

        public InsufficientFundsException(double accountBalance, double requiredBalance, string? message, Exception? innerException) : base(message, innerException)
        {
            AccountBalance = accountBalance;
            RequiredBalance = requiredBalance;
        }
    }
}
