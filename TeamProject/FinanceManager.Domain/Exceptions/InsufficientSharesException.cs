using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.Exceptions
{
    public class InsufficientSharesException : Exception
    {
        // symbol the user was trying to sell
        public string Symbol { get; }

        // amount of shares in the user's account
        public int AccountShares { get; }

        // the difference in shares the user needs to sell X amount of stocks
        public int RequiredShares { get; }

        /// <summary>
        /// Exception to handle when the user does not have enough shares.
        /// </summary>
        /// <param name="symbol">Symbol the user was trying to sell.</param>
        /// <param name="accountShares">Amount of shares of this symbol in the account.</param>
        /// <param name="requiredShares">Number of shares needed to sell the requested amount of shares.</param>
        public InsufficientSharesException(string symbol, int accountShares, int requiredShares)
        {
            Symbol = symbol;
            AccountShares = accountShares;
            RequiredShares = requiredShares;
        }

        public InsufficientSharesException(string? message, string symbol, int accountShares, int requiredShares) : base(message)
        {
            Symbol = symbol;
            AccountShares = accountShares;
            RequiredShares = requiredShares;
        }

        public InsufficientSharesException(string? message, Exception? innerException, string symbol, int accountShares, int requiredShares) : base(message, innerException)
        {
            Symbol = symbol;
            AccountShares = accountShares;
            RequiredShares = requiredShares;
        }
    }
}
