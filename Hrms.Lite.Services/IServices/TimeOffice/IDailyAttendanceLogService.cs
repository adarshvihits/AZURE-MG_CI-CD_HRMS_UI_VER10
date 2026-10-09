using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using Hrms.Lite.Shared.TimeOffice.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TimeOffice
{
    public interface IDailyAttendanceLogService
    {
        public Task<DailyAttendanceLog> DailyAttendanceLogList( string SelectedDate, Filter filter);
        public Task<ResponseEntity> DailyAttendanceLogSave(DailyAttendanceLog model);
        public Task<ResponseEntity> EmployeeWiseDailyAttendanceLogSave(DailyAttendanceLog model);
        public Task<ResponseEntity> DailyAttendanceLogBatchUpdate(DailyAttendanceLog model);
        public Task<DataSet> GetApplicantTrackingReport_Excel(string SelectedDate, Filter filter);

    }
}
