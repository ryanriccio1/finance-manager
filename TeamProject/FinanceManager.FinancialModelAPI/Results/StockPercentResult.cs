using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.FinancialModelAPI.Results
{
    public class StockPercentResult
    {
        [JsonProperty("1D")]
        public double _1D { get; set; }

        [JsonProperty("5D")]
        public double _5D { get; set; }

        [JsonProperty("1M")]
        public double _1M { get; set; }

        [JsonProperty("1Y")]
        public double _1Y { get; set; }
    }
}
