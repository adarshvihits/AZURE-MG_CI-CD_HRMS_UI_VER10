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
    public class ApplicantTrackingReportService: IApplicantTrackingReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public ApplicantTrackingReportService(IServiceBase httpService)
        {
            _baseURL = "RecruitmentReports";
            _httpService = httpService;
        }
        public async Task<RecruitmentReports> GetApplicantTrackingFileDetails(int ResumeDataBankCode)
        {
            return await _httpService.GetAsync<RecruitmentReports>($"{_baseURL}/GetApplicantTrackingFileDetails?ResumeDataBankCode={ResumeDataBankCode}");
        }

        public async Task<RecruitmentReports> GetApplicantTrackingReport(Filter filter1)
        {
            return await _httpService.PostAsync<RecruitmentReports,Filter>($"{_baseURL}/GetApplicantTrackingReport", filter1);
        }

        public async Task<DataSet> GetApplicantTrackingReport_Excel(Filter filter1)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetApplicantTrackingReport_Excel", filter1);
        }
    }
}
