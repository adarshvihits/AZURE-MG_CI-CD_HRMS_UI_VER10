
using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Shared;
using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank.Reports
{
    public class RecruitmentTrackerReportService : IRecruitmentTrackerReportService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;

        public RecruitmentTrackerReportService(IServiceBase httpService)
        {
            _baseURL = "RecruitmentTrackerReport";
            _baseURL1 = "ApplicantVerification";
            _httpService = httpService;
        }
        public async Task<RecruitmentTrackerReport> GetRecruitmentTrackerReport(Filter filter)
        {
            return await _httpService.PostAsync<RecruitmentTrackerReport, Filter>($"{_baseURL}/GetRecruitmentTrackerReport", filter);
        }
        public async Task<RecruitmentTrackerReport> GetDetails(Guid ApplicantGI)
        {
            return await _httpService.GetAsync<RecruitmentTrackerReport>($"{_baseURL}/GetDetails?ApplicantGI={ApplicantGI}");
        }
        public async Task<RecruitmentTrackerReport> GetTrackerDetails(Guid ApplicantGI)
        {
            return await _httpService.GetAsync<RecruitmentTrackerReport>($"{_baseURL}/GetTrackerDetails?ApplicantGI={ApplicantGI}");

        }
        public async Task<File> DownloadDoc(string DocType, int RowID, Guid ApplicantGI)
        {
            return await _httpService.GetAsync<File>($"{_baseURL}/DownloadDoc?DocType={DocType}&RowID={RowID}&ApplicantGI={ApplicantGI}");
        }
        public async Task<RecruitmentTrackerReport> GetDocumentPopup(string DocType, int RowID, Guid ApplicantGI)
        {
            return await _httpService.GetAsync<RecruitmentTrackerReport>($"{_baseURL1}/GetDocumentPopup?DocType={DocType}&RowID={RowID}&ApplicantGI={ApplicantGI}");
        }

        public async Task<DataSet> RecruitmentTrackerReport_Excel(string FromDate, string ToDate, Filter Filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/RecruitmentTrackerReport_Excel?FromDate={FromDate}&ToDate={ToDate}", Filter);

        }
    }
}

    
