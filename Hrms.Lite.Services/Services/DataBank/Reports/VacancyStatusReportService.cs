using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Shared.DataBank.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank.Reports
{
    public class VacancyStatusReportService : IVacancyStatusReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public VacancyStatusReportService(IServiceBase httpService)
        {
            _baseURL = "OnboardingVacancyStatusReport";
            _httpService = httpService;
        }
        public async Task<RecruitmentReports> GetOnboardingVacancyStatusReport()
        {
            return await _httpService.GetAsync<RecruitmentReports>($"{_baseURL}/GetOnboardingVacancyStatusReport");
        }

        public async Task<DataSet> GetOnboardingVacancyStatusReport_Excel()
        {
            return await _httpService.GetAsync<DataSet>($"{_baseURL}/GetOnboardingVacancyStatusReport_Excel");

        }
    }
}
