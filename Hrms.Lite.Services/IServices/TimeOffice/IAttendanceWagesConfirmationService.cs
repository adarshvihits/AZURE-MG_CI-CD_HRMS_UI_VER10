using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TimeOffice
{
   public interface IAttendanceWagesConfirmationService
    {
        Task<AttendanceWagesConfirmation> GetConfirmationList(string TabValue, int PGroup, int month, int ConfirmationStatus, Filter filter);
        Task<AttendanceWagesConfirmation> GetEmployeeWiseData(int month, string TabIndex,  int ConfirmationStatus,Guid EmployeeGI);
        Task<ResponseEntity> EmployeeWiseSave(AttendanceWagesConfirmation model);
        Task<ResponseEntity> AttendanceWagesConfirmationSave(AttendanceWagesConfirmation model);
        Task<AttendanceWagesConfirmation> EmployeeWiseDetails(string TabIndex, int month, int Value, Guid EmployeeGI);        
        public Task<DataSet> GetEmployeeAttnDetails_Excel(int month,string TabIndex,int ConfirmationStatus,Guid EmployeeGI);
        public Task<ResponseEntity> EmployeeWiseAttendanceWagesConfirmedValueUpdate(AttendanceWagesConfirmation model);
        Task<ResponseEntity> BatchUpdate( AttendanceWagesConfirmation model);


        ///Late hour Conf
        ///
        Task<ResponseEntity> LessHourConfirmationSave(Filter filter, int Month);
        public Task<ResponseEntity> EditUpdate(AttendanceWagesConfirmation model);
        Task<AttendanceWagesConfirmation> ConfirmationList_GEO(string Tab, Filter filter, int Month);
        Task<DataSet> GetConfirmationList_Excel(string Tab, Filter filter, int Month);

        Task<AttendanceWagesConfirmation> LHCDetails(int Month, int EmployeeCode);

    }
}
