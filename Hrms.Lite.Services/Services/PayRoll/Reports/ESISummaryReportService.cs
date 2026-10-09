using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll.Reports;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll.Reports;
using Hrms.Lite.Shared.TimeOffice;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.PayRoll.Reports
{
    public class ESISummaryReportService: IESISummaryReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public ESISummaryReportService(IServiceBase httpService)
        {
            _baseURL = "ESISummaryReport";
            _httpService = httpService;
        }
        public async Task<ESISummaryReport> GetESISummaryReport(int MonthCode, int ESIGroupCode,Filter filter)
        {
            return await _httpService.PostAsync<ESISummaryReport,Filter>($"{_baseURL}/GetESISummaryReport?Month={MonthCode}&ESIGroup={ESIGroupCode}", filter);
        }

       public async Task<DataSet> GetESISummaryReport_Excel(int MonthCode, int ESIGroupCode, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetESISummaryReport_Excel?Month={MonthCode}&ESIGroup={ESIGroupCode}", filter);

        }
    }
}
