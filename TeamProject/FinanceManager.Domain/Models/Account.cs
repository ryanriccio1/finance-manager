using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.Models
{
    /// <summary>
    /// Domain model to store information about a user's account.
    /// </summary>
    public class Account : DomainObject
    {
        // an account can have 1 user, but in other implementations there is
        // a possibility the User has more than one account.
        public User AccountHolder { get; set; }
        public double Balance { get; set; }
        public ICollection<AssetTransaction> AssetTransactions { get; set; }
    }
}
