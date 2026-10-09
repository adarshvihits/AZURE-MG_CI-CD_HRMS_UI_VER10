using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;   
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TimeOffice
{
    public interface IPeriodicLeaveCreditService
    {
        Task<PeriodicLeaveCredit> PeriodicLeaveCreditList(int LeavePeriod, int LeaveGroup, int Leave, Filter filter, int Month);
        Task<ResponseEntity> SavePeriodicLeaveCredit(int LeavePeriod, int LeaveGroup, int Leave, Filter Filter, int Month);
        Task<DataSet> PeriodicLeaveCreditList_Excel(int LeavePeriod, int LeaveGroup, int Leave, int Month, Filter filter);
        public Task<PeriodicLeaveCredit> PeriodicLeaveCreditHistoryList(int LeavePeriod, int Leave, int Month, int LeaveGroup, Filter filter);

        Task<DataSet> PeriodicLeaveCreditHistoryList_Excel(int LeavePeriod, int LeaveGroup, int Leave, int Month, Filter filter);



    }
}
