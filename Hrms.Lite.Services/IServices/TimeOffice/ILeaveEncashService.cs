using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TimeOffice
{
    public interface ILeaveEncashService
    {
        public Task<LeaveEncashment> LeaveEncashList(int LeavePeriod, int LeaveGroup, int Leave,int Month, int Type,Filter filter);
        public Task<LeaveEncashment> GetBatchUpDateDetails(int LeavePeriod, int LeaveGroup, int Leave,int Month);
        public Task<ResponseEntity> LeaveEncashBatchUpdate(LeaveEncashment model);
        public Task<LeaveEncashment> GetEditDetails(Guid EmployeeGI, int LeavePeriod, int LeaveGroup, int Leave, int Month);
       public Task<ResponseEntity> EmployeeWiseLeaveEncashModify(LeaveEncashment model);

        public Task<LeaveEncashment> LeaveEncashmentHistoryList( int LeavePeriod, int LeaveGroup, int Leave, Filter filter);
        /************************************** Leave EN Projection ******************************************************/
        public Task<LeaveEncashment> LeaveEncashmentProjection(int LeavePeriod, int Leave, Filter filter);
       public Task<DataSet> LeaveEncashmentProjection_Excel( int LeavePeriod, int Leave, Filter filter);
        public Task<DataSet> LeaveEncashmentList_Excel(int LeavePeriod, int LeaveGroup,int Leave,int Month, Filter filter);

    }
}

