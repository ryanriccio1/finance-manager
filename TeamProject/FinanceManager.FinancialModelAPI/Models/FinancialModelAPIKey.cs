using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.FinancialModelAPI.Models
{
    public class FinancialModelAPIKey
    {
        public string Key { get; }

        // store the key in class so that we can pass it around later through dependecy injection
        public FinancialModelAPIKey(string key) 
        {
            Key = key;
        }
    }
}
