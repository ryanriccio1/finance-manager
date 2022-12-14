using FinanceManager.Domain.Models;
using FinanceManager.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.Services.AuthenticationServices
{
    // store the different types of results we can get
    // when a user registers
    public enum RegistrationResult
    {
        Success,
        PasswordsDoNotMatch,
        EmailAlreadyExits,
        UsernameAlreadyExists
    }

    /// <summary>
    /// Service to authenticate with the database or create a new user.
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Register a new account with user's credentials.
        /// </summary>
        /// <param name="email">The user's email.</param>
        /// <param name="username">The user's username.</param>
        /// <param name="password">The user's password.</param>
        /// <param name="confirmPassword">A field confirming the user's password.</param>
        /// <returns>The result of the registration.</returns>
        /// <exception cref="Exception">Thrown if the registration fails.</exception>
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
        Task<Account> Login(string username, string password);
    }
}
