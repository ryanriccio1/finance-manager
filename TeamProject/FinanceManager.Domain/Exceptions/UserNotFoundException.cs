using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.Exceptions
{
    public class UserNotFoundException : Exception
    {
        // store the username that was not found
        // so it shows in the Exception
        public string Username { get; set; }

        /// <summary>
        /// Exception to handle when the User is not found.
        /// </summary>
        /// <param name="username">Username that was not found.</param>
        public UserNotFoundException(string username)
        {
            Username = username;
        }

        public UserNotFoundException(string? message, string username) : base(message)
        {
            Username = username;
        }

        public UserNotFoundException(string? message, Exception? innerException, string username) : base(message, innerException)
        {
            Username = username;
        }
    }
}
