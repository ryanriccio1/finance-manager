using FinanceManager.Domain.Models;
using FinanceManager.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Domain.Services.AuthenticationServices;

namespace FinanceManager.WPF.State.Authenticators
{
    /// <summary>
    /// Store the state of the autentication service
    /// </summary>
    public interface IAuthenticator
    {
        Account CurrentAccount { get; }
        bool IsLoggedIn { get; }

        event Action StateChanged;

        /// <summary>
        /// Register a user in our database.
        /// </summary>
        /// <param name="email">The user's email.</param>
        /// <param name="username">The user's username.</param>
        /// <param name="password">The use's password.</param>
        /// <param name="confirmPassword">The user input confirming their entered password.</param>
        /// <returns>The result of the registration</returns>
        Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword);

        /// <summary>
        /// Get an account for a user's credentials.
        /// </summary>
        /// <param name="username">The user's username.</param>
        /// <param name="password">The user's password.</param>
        /// <returns>The account for the user.</returns>
        /// <exception cref="UserNotFoundException">Thrown if the user does not exist.</exception>
        /// <exception cref="InvalidPasswordException">Thrown if the password is invalid.</exception>
        /// <exception cref="Exception">Thrown if the login fails.</exception>
        Task Login(string username, string password);
        void Logout();
    }
}
