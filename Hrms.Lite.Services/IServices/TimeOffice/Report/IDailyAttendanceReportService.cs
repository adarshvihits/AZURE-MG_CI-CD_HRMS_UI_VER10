using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TimeOffice.Report
{
    public interface IDailyAttendanceReportService
    {
        //Task<DailyAttendanceEntry> GetDailyAttendanceReportList(string FromDate, string ToDate, int TypeCode, string StatusCode, Filter filterdata);
        Task<DailyAttendanceEntry> GetDailyAttendanceReportList(DailyAttendanceEntry model);
        // Task<DataSet> DailyAttendanceReport_Excel(string FromDate, string ToDate, int TypeCode, int StatusCode, Filter filterdata);
        Task<DataSet> DailyAttendanceReport_Excel(DailyAttendanceEntry model);
    }
}
