using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using Hrms.Lite.Shared.TimeOffice.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TimeOffice.Report
{
    public interface IMonthlyAttendanceSummaryReportService
    {
        public Task<MonthlyAttendanceSummaryReport> MonthlyAttendanceSummReport(int Month, Filter filterdata);
        Task<DataSet> MonthlyAttendanceSummaryReport_Excel(int Month, Filter filterdata);

    }
}
