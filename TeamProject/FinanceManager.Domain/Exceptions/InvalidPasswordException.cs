using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.Exceptions
{
    public class InvalidPasswordException : Exception
    {
        // store the username and password inputted 
        // for an account with an invalid password
        public string Username { get; set; }
        public string Password { get; set; }

        /// <summary>
        /// Exception to handle when the password is incorrect.
        /// </summary>
        /// <param name="username">Username of account with incorrect password.</param>
        /// <param name="password">Incorrect password that was entered.</param>
        public InvalidPasswordException(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public InvalidPasswordException(string? message, string username, string password) : base(message)
        {
            Username = username;
            Password = password;
        }

        public InvalidPasswordException(string? message, Exception? innerException, string username, string password) : base(message, innerException)
        {
            Username = username;
            Password = password;
        }
    }
}
