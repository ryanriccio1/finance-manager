using FinanceManager.Domain.Exceptions;
using FinanceManager.Domain.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FinanceManager.Domain.Services.AuthenticationServices.IAuthenticationService;

namespace FinanceManager.Domain.Services.AuthenticationServices
{
    public class AuthenticationService : IAuthenticationService
    {
        // service to get the account from the database
        private readonly IAccountService _accountService;

        // used when storing or verifying a password in the database
        private readonly IPasswordHasher _passwordHasher;

        // instantiate members
        public AuthenticationService(IAccountService accountService, IPasswordHasher passwordHasher)
        {
            _accountService = accountService;
            _passwordHasher = passwordHasher;
        }

        public async Task<Account> Login(string username, string password)
        {
            // get the account by the inputted username
            Account storedAccount = await _accountService.GetByUsername(username);

            // if we get no account
            if (storedAccount == null)
            {
                throw new UserNotFoundException(username);
            }

            // if we have a valid account, hash the inputted password
            PasswordVerificationResult passwordResult = _passwordHasher.VerifyHashedPassword(storedAccount.AccountHolder.PasswordHash, password);

            // if the hash is incorrect, we have the wrong password
            if (passwordResult != PasswordVerificationResult.Success) 
            {
                throw new InvalidPasswordException(username, password);
            }

            // once we have authenticated, return the account
            return storedAccount;
        }

        public async Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword)
        {
            // store the state of the registration
            RegistrationResult result = RegistrationResult.Success;

            // if the passwords do not match, change the state
            if (password != confirmPassword)
            {
                result = RegistrationResult.PasswordsDoNotMatch;
            }

            // see if any accounts exist under the same email, if they do, change the state
            Account emailAccount = await _accountService.GetByEmail(email);
            if (emailAccount != null) 
            {
                result = RegistrationResult.EmailAlreadyExits;
            }

            // see if any accounts exist under the same email, if they do, change the state
            Account usernameAccount = await _accountService.GetByUsername(username);
            if (usernameAccount != null)
            {
                result = RegistrationResult.UsernameAlreadyExists;
            }

            // if we are still successful
            if (result == RegistrationResult.Success) 
            {
                // hash the password
                string hashedPassword = _passwordHasher.HashPassword(password);

                // store to a new user
                User user = new User()
                {
                    Email = email,
                    Username = username,
                    PasswordHash = hashedPassword,
                    DateJoined = DateTime.Now
                };

                // link account to user and set a new
                // default balance of $5000
                Account account = new Account()
                {
                    AccountHolder = user,
                    Balance = 5000
                };

                // create the account in the database
                await _accountService.Create(account);
            }

            return result;
        }
    }
}
