using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.Exceptions
{
    public class InvalidSymbolException : Exception
    {
        // store the symbol that was not found
        // so it shows in the Exception
        public string Symbol { get; set; }

        /// <summary>
        /// Exception to handle when the stock Symbol is not found.
        /// </summary>
        /// <param name="symbol">Symbol that was not found.</param>
        public InvalidSymbolException(string symbol)
        {
            Symbol = symbol;
        }

        public InvalidSymbolException(string symbol, string? message) : base(message)
        {
            Symbol = symbol;
        }

        public InvalidSymbolException(string symbol, string? message, Exception? innerException) : base(message, innerException)
        {
            Symbol = symbol;
        }
    }
}
