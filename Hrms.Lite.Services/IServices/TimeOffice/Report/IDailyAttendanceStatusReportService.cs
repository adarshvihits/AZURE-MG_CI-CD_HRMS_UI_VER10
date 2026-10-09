using Hrms.Lite.Shared.TimeOffice.Report;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TimeOffice.Report
{
    public interface IDailyAttendanceStatusReportService
    {
        Task<System.Data.DataSet> GetDetails(DailyAttendanceStatusReport model);
    }
}
