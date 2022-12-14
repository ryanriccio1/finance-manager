using FinanceManager.Domain.Models;
using FinanceManager.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.Services.TransactionServices
{
    public interface IBuyStockService
    {
        /// <summary>
        /// Buy a stock for an account.
        /// </summary>
        /// <param name="buyer">The account of the buyer.</param>
        /// <param name="symbol">The symbol bought.</param>
        /// <param name="shares">The amount of shares to buy.</param>
        /// <returns>The updated account.</returns>
        /// <exception cref="InvalidSymbolException">Thrown if the purchased symbol is invalid.</exception>
        /// <exception cref="InsufficientFundsException">Thrown if the user does not have enough money.</exception>
        /// <exception cref="Exception">Thrown if the transaction fails.</exception>
        public Task<Account> BuyStock(Account buyer, string symbol, int shares);
    }
}
