using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.Models
{
    // Store the types of Major Indexes that we support.
    public enum MajorIndexType
    {
        DowJones,
        Nasdaq,
        SP500
    }

    /// <summary>
    /// Domain model to store information about a Stock index.
    /// </summary>
    public class MajorIndex
    {
        public string IndexName { get; set; }
        public double Price { get; set; }
        public double Changes { get; set; }
        public MajorIndexType Type { get; set; }
    }
}
