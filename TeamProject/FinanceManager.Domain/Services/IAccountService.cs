using FinanceManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.Services
{
    /// <summary>
    /// CRUD interface for an Account.
    /// </summary>
    public interface IAccountService : IDataService<Account>
    {
        /// <summary>
        /// Service to get account information by username.
        /// </summary>
        /// <param name="username">Username to search for.</param>
        /// <returns>Account if found.</returns>
        Task<Account> GetByUsername(string username);
        
        /// <summary>
        /// Service to get account information by email.
        /// </summary>
        /// <param name="email">Email to search for.</param>
        /// <returns>Account if found.</returns>
        Task<Account> GetByEmail(string email);
    }
}
