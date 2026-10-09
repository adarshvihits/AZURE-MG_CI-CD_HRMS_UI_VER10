using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TimeOffice
{
   public interface IDailyAttendanceEntryService
    {

        public Task<DailyAttendanceEntry> DailyAttendanceEntryList( int Month, int PayrollGroup, int Tabindex, Filter filter);
        Task<DataSet> DailyAttendanceEntryList_Excel(int Month, int PayrollGroup, int Tabindex, Filter filterdata);

        public Task<DailyAttendanceEntryPaging> DailyAttendanceEntryPagingList(int Month, int PayrollGroup, int Tabindex, Filter filter);
        public Task<DailyAttendanceEntry> DailyAttendanceEntryEditList( string Date, int PayrollGroup, int AttnStatus, Filter filter);
        public Task<DailyAttendanceEntry> DailyAttendanceEntryEditDetails( string Date, Guid EmployeeGI);
        public Task<DailyAttendanceEntry> EmployeeDailyAttendanceEntryEditDetails(int Month,int ABSTATUS, Guid EmployeeGI);
        public Task<DailyAttendanceEntry> EmployeeWiseDailyAttendanceEntryList( int Month, Filter filter, int PgGroup);
        public Task<ResponseEntity> DailyattendanceEntry(DailyAttendanceEntry model);
        public Task<ResponseEntity> DailyattendanceEntryBatchUpdate(DailyAttendanceEntry model);
        public Task<ResponseEntity> EmployeeWiseAttendanceEntryBatchUpdate(DailyAttendanceEntry model);
        Task<DataSet> EmployeeWiseDailyAttendanceEntryList_Excel(int Month, Filter filter, int PgGroup);

        Task<ResponseEntity> GetDailyAttendanceEntryDateValidation(string SelectedDate, int? PayrollGroup, int? EmployeeCode);

        public Task<DailyAttendanceEntry> EmployeeLeaveDetails(int Month, Guid EmployeeGI);
        //*******************************************DailyAttendanceProcessing****************************************************************************
        public Task<DailyAttendanceEntry> DailyAttendanceProcessing( string Date, Filter filterdata);
        public Task<DailyAttendanceEntry> DailyAttendanceProcessing_Details(string Date, int StatusCode, Filter filterdata);
        public Task<DataSet> DailyAttendanceProcessing_Details_Excel(string Date, int StatusCode, Filter filterdata);
        public Task<ResponseEntity<DailyAttendanceEntry>> DailyAttendanceProcessingValidation(string SelectedDate);


        //*********************Daily attendance processing-individual*********************

        Task<DailyAttendanceEntry> Individual_DailyAttendanceProcessing(string Date, int EmployeeCode);

    }
}
