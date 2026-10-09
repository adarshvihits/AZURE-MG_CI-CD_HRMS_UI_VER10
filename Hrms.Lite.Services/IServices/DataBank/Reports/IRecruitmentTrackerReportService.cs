
using Hrms.Lite.Shared;
using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank.Reports
{
    public interface IRecruitmentTrackerReportService
    {
        public Task<RecruitmentTrackerReport> GetRecruitmentTrackerReport(Filter filter);
        public Task<RecruitmentTrackerReport> GetDetails(Guid ApplicantGI);
        public Task<RecruitmentTrackerReport> GetTrackerDetails(Guid ApplicantGI);
        public Task<File> DownloadDoc(string DocType, int RowID, Guid ApplicantGI);
        public Task<RecruitmentTrackerReport> GetDocumentPopup(string DocType, int RowID, Guid ApplicantGI);
        public Task<DataSet> RecruitmentTrackerReport_Excel(string FromDate, string ToDate, Filter Filter);



    }
}
