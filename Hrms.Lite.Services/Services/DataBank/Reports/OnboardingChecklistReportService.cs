using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank.Reports
{
    public class OnboardingChecklistReportService: IOnboardingChecklistReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public OnboardingChecklistReportService(IServiceBase httpService)
        {
            _baseURL = "OnboardingChecklistReport";
            _httpService = httpService;
        }

        public async Task<OnboardingChecklistReport> GetOnboardingChecklistReport(string FromDate, string ToDate, Filter filter)
        {
            return await _httpService.PostAsync<OnboardingChecklistReport, Filter>($"{_baseURL}/GetOnboardingChecklistReport?FromDate={FromDate}&ToDate={ToDate}", filter);
        }
        public async Task<DataSet> GetOnboardingChecklistReport_Excel(string FromDate, string ToDate, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetOnboardingChecklistReport_Excel?FromDate={FromDate}&ToDate={ToDate}", filter);
        }
        public async Task<OnboardingChecklistReport> GetOnboardingChecklistDetails(int EmployeeCode)
        {
            return await _httpService.GetAsync<OnboardingChecklistReport>($"{_baseURL}/GetOnboardingChecklistDetails?EmployeeCode={EmployeeCode}");
        }
        public async Task<DataSet> GetOnboardingChecklistDetails_Excel(int EmployeeCode)
        {
            return await _httpService.GetAsync<DataSet>($"{_baseURL}/GetOnboardingChecklistDetails_Excel?EmployeeCode={EmployeeCode}");
        }
    }
}
