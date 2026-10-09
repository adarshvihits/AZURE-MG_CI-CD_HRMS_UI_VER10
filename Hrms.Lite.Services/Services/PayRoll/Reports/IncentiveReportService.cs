using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll.Reports;
using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.PayRoll.Reports
{
    public class IncentiveReportService : IIncentiveReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public IncentiveReportService(IServiceBase httpService)
        {
            _baseURL = "IncentiveReport";
            _httpService = httpService;
        }

        public Task<System.Data.DataSet> GetDetails(int SalaryPeriod, IncentiveReport model)
        {
            return _httpService.PostAsync<System.Data.DataSet, IncentiveReport>($"{_baseURL}/GetIncentiveReport?SalaryPeriodCode={SalaryPeriod}", model);
        }

        public Task<System.Data.DataSet> GetPaySheetDraftDetails(int SalaryPeriod, PaySheet model)
        {
            return _httpService.PostAsync<System.Data.DataSet, PaySheet>($"{_baseURL}/GetPaySheetDraft?SalaryPeriodCode={SalaryPeriod}", model);
        }
    }
}
