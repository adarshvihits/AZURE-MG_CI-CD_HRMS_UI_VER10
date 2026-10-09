using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TimeOffice
{
    public interface IAttendanceSettingService
    {
        Task<AttendanceSetting> AttendanceSettingList(int GroupBy, int Classification,int TypeCode, Filter model);
        Task<ResponseEntity> AttendanceSettingSave(AttendanceSetting model);
        public Task<AttendanceSetting> GetAttendanceSettingsEditDetails(Guid EmpGI);
        public Task<DataSet> GetList_Excel(int GroupBy, int Classification, int TypeCode, Filter model);
    }
}
