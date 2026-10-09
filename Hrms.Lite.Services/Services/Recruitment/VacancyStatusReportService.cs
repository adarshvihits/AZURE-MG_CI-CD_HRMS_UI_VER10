using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Recruitment;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Recruitment.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.Recruitment
{
    public class VacancyStatusReportService: IVacancyStatusReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public VacancyStatusReportService(IServiceBase httpService)
        {
            _baseURL = "RecruitmentReports";
            _httpService = httpService;
        }
        public async Task<RecruitmentReports> GetVacancyStatusReport(Filter filter)
        {
            return await _httpService.PostAsync<RecruitmentReports,Filter>($"{_baseURL}/GetVacancyStatusReport", filter);
        }

        public async Task<DataSet> GetVacancyStatusReport_Excel(Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetVacancyStatusReport_Excel", filter);
        }
    }
}
