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
    public class LWFReportService : ILWFReportService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;

        private readonly IServiceBase _httpService;
        public LWFReportService(IServiceBase httpService)
        {
            _baseURL = "LWFReport";
            _baseURL1 = "LWFDetailsReport";
            _httpService = httpService;
        }

        public async Task<LWFReport> GetList(int Month, int DeductionCode, Filter filter)
        {
            return await _httpService.PostAsync<LWFReport, Filter>($"{_baseURL}/GetReportList?Month={Month}&DeductionCode={DeductionCode}",filter);
        }


        public async Task<LWFReport> GetLWFReport(int Month, int DeductionCode)
        {
            return await _httpService.GetAsync<LWFReport>($"{_baseURL1}/GetLWFReportPrint?Month={Month}&DeductionCode={DeductionCode}");
        }


        public async Task<DataSet> LWFReport_Excel(int Month, int DeductionCode, Filter Filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/LWFReport_Excel?Month={Month}&DeductionCode={DeductionCode}", Filter);
        }

    }
}



