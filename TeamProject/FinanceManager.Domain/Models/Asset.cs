using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.Models
{
    /// <summary>
    /// Domain model to store information about a stock or asset.
    /// </summary>
    public class Asset
    {
        public string Symbol { get; set; }
        public double PricePerShare { get; set; }
    }
}
