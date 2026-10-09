using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TimeOffice
{
    public interface IMonthlyAttendanceProcessingService
    {
        public Task<MonthlyAttendanceProcessing> MonthlyAttendanceProcessingAbbreviation();
        public Task<MonthlyAttendanceProcessing> MonthlyAttendanceProcessingList(  int Month, int PayrollGroup, int Tabindex, Filter filterdata);
        public Task<MonthlyAttendanceProcessing> MonthlyAttendanceProcessingListPopUp(int Month, int PayrollGroup, int Tabindex, string Description, Filter filterdata);

        public Task<ResponseEntity> ProcessingSave( int Month, int PayrollGroup, int Tabindex, Filter filterdata);
        Task<DataSet> MonthlyAttendanceProcessingList_Excel(int Month, int PayrollGroup, int Tabindex, Filter filterdata);
        Task<DataSet> MonthlyAttendanceProcessingApprovalPendingPopUp_Excel(int Month, int PayrollGroup, int Tabindex, Filter filterdata);
        Task<MonthlyAttendanceProcessing> GetBlankStatDaysPopList(Guid EmployeeGI, int Month);
        public Task<ResponseEntity> ProcessingPeriodLock(int Month, int PayrollGroup, int Tabindex, Filter filterdata);
        public Task<MonthlyAttendanceProcessing> MonthlyAttendanceProcessingApprovalPendingPopUp(int Month, int PayrollGroup, int Tabindex, Filter filterdata);


        /***************************************************** individual Attendance processing ********************************************/
        public Task<MonthlyAttendanceProcessing> Individual_MonthlyAttendanceProcessingList( int Month, int EmployeeCode, int Tabindex); /*if tabindex==0, validation  tabindex==1 , 
                                                                                                                                          * prosessing list*/
        public Task<ResponseEntity> Individual_ProcessingSave(MonthlyAttendanceProcessing model);
    }
}
