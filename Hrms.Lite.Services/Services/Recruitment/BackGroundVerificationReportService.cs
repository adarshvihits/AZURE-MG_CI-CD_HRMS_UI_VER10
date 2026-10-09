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
    public class BackGroundVerificationReportService: IBackGroundVerificationReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public BackGroundVerificationReportService(IServiceBase httpService)
        {
            _baseURL = "RecruitmentReports";
            _httpService = httpService;
        }

        public async Task<BackGroundVerificationReport> GetBackGroundVerificationReport(Filter filter)
        {
            return await _httpService.PostAsync<BackGroundVerificationReport, Filter>($"{_baseURL}/BackGroundVerificationReportList", filter);
        }
        public async Task<DataSet> GetBackGroundVerificationReport_Excel(Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetBackGroundVerificationReport_Excel", filter);
        }
    }
}
