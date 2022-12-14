using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.Models
{
    /// <summary>
    /// Domain model to store information about the User.
    /// </summary>
    public class User : DomainObject
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public DateTime DateJoined { get; set; }
    }
}
