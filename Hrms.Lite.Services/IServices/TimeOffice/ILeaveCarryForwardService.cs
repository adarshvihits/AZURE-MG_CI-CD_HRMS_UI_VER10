using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TimeOffice
{
    public interface ILeaveCarryForwardService
    {
        public Task<LeaveCarryForward> LeaveCarryForwardList(int LeavePeriodFrom, int LeavePeriodTo, int LeaveGroup, int Leave, int Month,int EntryType ,Filter filter);
        public Task<LeaveCarryForward> GetBatchUpDateDetails(int LeavePeriodFrom, int LeavePeriodTo, int LeaveGroup, int Leave, int Month);
        public Task<LeaveCarryForward> GetEditDetails(Guid EmployeeGI, int LeavePeriodFrom, int LeavePeriodTo, int LeaveGroup, int Leave, int Month);
        public Task<ResponseEntity> EmployeeWiseCFSave(LeaveCarryForward model);
        public Task<ResponseEntity> LeaveCFBatchUpdate(LeaveCarryForward model);
        public Task<ResponseEntity> LeaveCFSave(LeaveCarryForward model);
        Task<DataSet> LeaveCarryForwardList_Excel(int LeavePeriodFrom, int LeavePeriodTo, int LeaveGroup, int Leave, int Month, Filter filter, int Type);


        public Task<LeaveCarryForward> LeaveCarryForwardHistoryList(int LeavePeriodFrom, int LeavePeriodTo, int LeaveGroup, int Leave, Filter filter);

        Task<DataSet> LeaveCarryForwardHistoryList_Excel(int LeavePeriodFrom, int LeavePeriodTo, int LeaveGroup, int Leave, Filter filter);

    }
}
