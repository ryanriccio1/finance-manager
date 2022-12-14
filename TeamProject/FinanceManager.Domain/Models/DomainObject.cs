using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.Models
{
    /// <summary>
    /// The base class for any entity in the database.
    /// </summary>
    public class DomainObject
    {
        // every item in the database will have an Id
        public int Id { get; set; }
    }
}
