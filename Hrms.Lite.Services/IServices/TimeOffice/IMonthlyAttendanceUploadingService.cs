using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TimeOffice
{
     public interface IMonthlyAttendanceUploadingService
    {
        Task<MonthlyAttendanceUploading> GetEmployeeList( int Month, int PayrollGroup,Filter filter);

        Task<ResponseEntity<MonthlyAttendanceUploading>> ImportFromExcel(int Month, int PayrollGroup, MonthlyAttendanceUploading model);
        Task<ResponseEntity> Update(int Month, MonthlyAttendanceUploading_Data input);
        Task<DataSet> GetEmployeeList_Excel(int Month, int PayrollGroup,Filter filter);

    }
}
