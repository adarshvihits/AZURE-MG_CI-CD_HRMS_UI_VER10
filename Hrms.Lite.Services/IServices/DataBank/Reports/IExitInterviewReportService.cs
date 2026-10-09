using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank.Reports
{
    public interface IExitInterviewReportService
    {
        public Task<ExitInterviewReport> GetExitInterviewReportList(Filter filter);
        Task<DataSet> GetExitInterviewReport_Excel(Filter filter);
        Task<ExitInterviewReport> GetExitInterviewReportPopUpDetails(int EmployeeCode);
        Task<DataSet> GetExitInterviewReportPopUp_Excel(int EmployeeCode);
    }
}
