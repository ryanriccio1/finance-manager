using FinanceManager.Domain.Models;
using FinanceManager.Domain.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace FinanceManager.FinancialModelAPI.Services
{
    public class MajorIndexService : IMajorIndexService
    {
        private readonly FinancialModelAPIHttpClient _client;

        public MajorIndexService(FinancialModelAPIHttpClient client)
        {
            _client = client;
        }

        public async Task<MajorIndex> GetMajorIndex(MajorIndexType indexType)
        {
            string uri = "majors-indexes/" + GetUriSuffix(indexType);

            // deserialize the ressponse into a MajorIndex class
            MajorIndex majorIndex = await _client.GetAsync<MajorIndex>(uri);
            majorIndex.Type = indexType;

            return majorIndex;
        }

        private string GetUriSuffix(MajorIndexType indexType) 
        {
            switch (indexType)
            {
                // using our Enum, get the text representation of the major index that the api will recognize
                case MajorIndexType.DowJones:
                    return ".DJI";
                case MajorIndexType.Nasdaq:
                    return ".IXIC";
                case MajorIndexType.SP500:
                    return ".INX";
                default:
                    throw new Exception("MajorIndexType does not have a suffix defined.");
            }
        }
    }
}
