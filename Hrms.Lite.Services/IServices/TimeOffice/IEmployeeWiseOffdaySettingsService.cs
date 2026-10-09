using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TimeOffice
{
    public interface IEmployeeWiseOffdaySettingsService
    {
        public Task<EmployeeWiseOffdaySettings> GetOffdaySettingsList(int? year,int ShiftType, Filter filter);
        public Task<EmployeeWiseOffdaySettings> GetOffdaySettingsBatchUpdateDetails(int YearCode,int ShiftType);
        public Task<EmployeeWiseOffdaySettings> GetOffdaySettingsEditDetails(int YearCode, Guid EmpGI);
        public Task<EmployeeWiseOffdaySettings> GetEmployeeOffdaySettingsDetailstPopUp(int YearCode, Guid EmpGI);
        public Task<ResponseEntity> BatchUpdateSave(EmployeeWiseOffdaySettings model);
        public Task<ResponseEntity> EmployeeWiseSave(EmployeeWiseOffdaySettings model);
        Task<DataSet> GetOffdaySettingsList_Excel(int Year, int ShiftTypeCode,Filter filter);

    }
}
