using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Recruitment.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Recruitment
{
    public interface IApplicantTrackingReportService
    {
        public Task<RecruitmentReports> GetApplicantTrackingReport(Filter filter1);
        public Task<RecruitmentReports> GetApplicantTrackingFileDetails(int ResumeDataBankCode);
        public Task<DataSet> GetApplicantTrackingReport_Excel(Filter filter1);
    }
}
