using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TrainingModule;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TrainingModule
{
 public  interface IAttendanceEntryMobilePunchService
    {
        public Task<AttendanceEntryMobilePunch> GetScheduleList(int ScheduleCode, Filter filter, int radiomode,int radio1);
        public Task<ResponseEntity> Save(int ScheduleCode, AttendanceEntryMobilePunch model, int radiomode);
        public Task<AttendanceEntryMobilePunch> GetTopicsPopUp(int ScheduleCode, int EmployeeCode);
    }
}
