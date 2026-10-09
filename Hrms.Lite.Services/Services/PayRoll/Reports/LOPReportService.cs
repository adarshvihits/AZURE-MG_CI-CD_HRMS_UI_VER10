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
   public class LOPReportService: ILOPReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public LOPReportService(IServiceBase httpService)
        {
            _baseURL = "LOPReport";
            _httpService = httpService;
        }
        public async Task<LOPReport> GetList(int Month, int Type)
        {
            return await _httpService.GetAsync<LOPReport> ($"{_baseURL}/GetReportList?Month={Month}&Type={Type}");
        }

        public async Task<DataSet> GetLOPReport_Excel(int Month, int Type, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetLOPReport_Excel?Month={Month}&Type={Type}", filter);

        }

    }
}
