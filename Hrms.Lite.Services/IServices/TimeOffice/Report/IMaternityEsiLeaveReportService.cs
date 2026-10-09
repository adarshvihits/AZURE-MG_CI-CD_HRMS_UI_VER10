using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice.Report;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TimeOffice.Report
{
 public  interface IMaternityEsiLeaveReportService
    {
        Task<System.Data.DataSet> GetMaternityEsiLeaveReport_Excel( int LeaveCode,Filter filterdata);
        Task<MaternityEsiLeaveReport> GetMaternityEsiLeaveReport(int LeaveCode,Filter filterdata);
    }
}
