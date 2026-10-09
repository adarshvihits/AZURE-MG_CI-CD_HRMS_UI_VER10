using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll.Reports;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.PayRoll.Reports
{
   public class PFSummaryReportService : IPFSummaryReportService
    {
        private readonly string _baseURL;
        private readonly string _baseURL2;
        private readonly IServiceBase _httpService;
        public PFSummaryReportService(IServiceBase httpService)
        {
            _baseURL = "PFSummaryReport";
            _baseURL2 = "Common";
            _httpService = httpService;
        }
        public async Task<PFSummaryReport> GetList(int Month, int PFGroup, Filter filter)
        {
            return await _httpService.PostAsync<PFSummaryReport,Filter>($"{_baseURL}/GetPFSummaryReport?Month={Month}&PFGroup={PFGroup}",filter);
        }

        public async Task<Url> GetOfferLetter(string Type)
        {
            return await _httpService.GetAsync<Url>($"{_baseURL2}/GetOfferLetter?Type={Type}");
        }

        public  async Task<DataSet> GetPFSummaryReport_Excel( int Month, int PFGroup, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetPFSummaryReport_Excel?Month={Month}&PFGroup={PFGroup}", filter);
        }

        public async  Task<Url> GetUrl()
        {
            return await _httpService.GetAsync<Url>($"{_baseURL2}/GetAPIUrl");
        }
    }
}
