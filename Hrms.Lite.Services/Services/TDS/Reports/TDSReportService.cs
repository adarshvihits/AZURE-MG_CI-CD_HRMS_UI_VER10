using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TDS.Reports;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TDS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.TDS.Reports
{
   public class TDSReportService: ITDSReportService
    {

        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public TDSReportService(IServiceBase httpService)
        {
            _baseURL = "TDSProcessing";
            _httpService = httpService;
        }
      
        public async Task<TDSProcessing> GetTDSReport(Filter filter,int Month)
        {
            return await _httpService.PostAsync<TDSProcessing, Filter>($"{_baseURL}/GetTDSReport?Month={Month}", filter);
        }
        public async Task<DataSet> GetTDSReport_Export(Filter filter, int Month)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetTDSReport_Export?Month={Month}", filter);
        }

    }
}
