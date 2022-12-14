using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.FinancialModelAPI.Results
{
    public class StockPriceResult
    {   // store this in a property so that it can be deserialized from a json repsonse
        public double Price { get; set; }
    }
}
