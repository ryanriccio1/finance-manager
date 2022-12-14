using FinanceManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.Services
{
    /// <summary>
    /// Service to get info about major indexes.
    /// </summary>
    public interface IMajorIndexService
    {
        /// <summary>
        /// Get a major index.
        /// </summary>
        /// <param name="indexType">The type of index to get.</param>
        /// <returns>Information about a major index.</returns>
        Task<MajorIndex> GetMajorIndex(MajorIndexType indexType);
    }
}
