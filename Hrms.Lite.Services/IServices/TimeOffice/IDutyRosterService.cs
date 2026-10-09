using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TimeOffice
{
  public  interface IDutyRosterService
    {
        public Task<ShiftDetails> GetShiftDetails(int ShiftCode);
        public Task<IList<ShiftDetails>> GetShiftDetailsforPopUp();
        public Task<DutyRoster> DutyRosterDateWiseEntryList( int Month, int PayrollGroup, string Date, int CIRCLE, Filter filterdata);
        public Task<DutyRoster> DutyRosterEntryList(int Month, int PayrollGroup,int CIRCLE, Filter filterdata);
        Task<DataSet> DutyRosterEntryList_Excel(int Month, int PayrollGroup, int CIRCLE,int ShitType, Filter filterdata);

        public Task<DutyRoster> DutyRosterMailListBatchUpdate_Details(int Month, int PayrollGroup, string FROM, string TO, int CIRCLE, int ShiftCode, Filter filterdata);
        public Task<DutyRoster> DutyRosterDateWiseEntryList_BatchUpdate_Details( int Month, int PayrollGroup, string Date, int CIRCLE, Filter filterdata);
        public Task<DutyRoster> DutyRosterEmployeeWiseEntry_BatchUpdate_Details( int Month);
        public Task<DutyRoster> EmployeeDutyRosterEditDetails(int Month, int PayrollGroup, string FROM, string TO, int CIRCLE, Guid EmployeeGI);

        public Task<ResponseEntity> DutyRosterSave(Employee_DutyRosterEditDetails model);
        public Task<ResponseEntity> DutyRoster_BatchUpDate(DutyRoster model);
        public Task<ResponseEntity> DutyRoster_Employeewise_BatchUpDate(DutyRoster model);
        public Task<ResponseEntity> DutyrosterLockCheck(int CircleCode, string SelectedDate);

       public Task<DutyRoster> DutyRosterEntryList_Paging(int Month, int PayrollGroup, int CIRCLE,int ShitType,Filter filterdata);
        public Task<ResponseEntity<List<DutyRosterList>>> ImportFromExcel(DutyRoster model);
        public Task<ResponseEntity<List<DutyRosterList>>> ImportFromExcel_FromToDateUpdation(DutyRoster model);
    }
}
