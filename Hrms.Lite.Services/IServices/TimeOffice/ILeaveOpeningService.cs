using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TimeOffice
{
    public interface ILeaveOpeningService
    {
        public Task<LeaveOpening> LeaveOpeningList(int LeavePeriod, int LeaveGroup, int Leave,int Type,Filter Filter);
        public Task<LeaveOpening> GetBatchUpDateDetails(int LeavePeriod, int LeaveGroup, int Leave);
        public Task<LeaveOpening> GetEditDetails(Guid EmployeeGI, int LeavePeriod, int LeaveGroup, int Leave);
        public Task<ResponseEntity> LeaveOpeningSave(LeaveOpening model);
        public Task<ResponseEntity> EmployeeWiseLeaveOpeningSave(LeaveOpening model);
        public Task<ResponseEntity> LeaveOpeningBatchUpdate(LeaveOpening model);
        public Task<LeaveOpening> LeaveOpeningHistoryList( int LeavePeriod, int LeaveGroup, int Leave, Filter filter);     
        public Task<LeaveOpening> LeaveOpeningDeleteList( int LeavePeriod, int LeaveGroup, int Leave, Filter filter);
        public Task<DataSet> LeaveOpeningList_Excel(int LeavePeriod, int LeaveGroup, int Leave, int Type, Filter filter);
        public Task<ResponseEntity> Delete(LeaveOpening model);
        public Task<ResponseEntity> DeleteAll(LeaveOpening model);
        Task<DataSet> LeaveOpeningHistoryList_Excel(int LeavePeriod, int LeaveGroup, int Leave, Filter filter);
        public Task<ResponseEntity<List<LeaveOpening>>> ImportFromExcel(LeaveOpening model);
    }
}
