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
    public class ReferralIncentiveReportService : IReferralIncentiveReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public ReferralIncentiveReportService(IServiceBase httpService)
        {
            _baseURL = "ReferralIncentiveReport";
            _httpService = httpService;
        }

        public async Task<ReferralIncentiveReport> GetList(FilterMultiSelect model, int MonthCode)
        {
            return await _httpService.GetAsync<ReferralIncentiveReport>($"{_baseURL}/GetList?MonthCode={MonthCode}");
        }

        public async Task<DataSet> GetList_Excel(int MonthCode)
        {
            return await _httpService.GetAsync<DataSet>($"{_baseURL}/GetList_Excel?MonthCode={MonthCode}");
        }
    }
}
