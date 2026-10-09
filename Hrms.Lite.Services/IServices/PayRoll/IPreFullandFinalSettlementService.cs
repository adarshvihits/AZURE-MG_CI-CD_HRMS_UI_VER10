
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll
{
 public interface IPreFullandFinalSettlementService
    {
        public Task<PreFullandFinalSettlement> GetList(int Month, int PayrollGroup, Filter filter);
        public Task<PreFullandFinalSettlement> GetPreFandFDetails(Guid EmployeeGI, int Month);
        public Task<ResponseEntity> PreFandFSave(PreFullandFinalSettlement model);
        public Task<DataSet> GetList_EXCEL(int Month, int PayrollGroupCode, Filter filter);

        //----Pre F and F Approval
        public Task<PreFullandFinalSettlement> GetApprovalList(int Month, int PayrollGroup, Filter filter);
        public Task<PreFullandFinalSettlement> PreFandFApprovalDetails(Guid EmployeeGI, int Month);
        public Task<ResponseEntity> PreFandFApproveOrReject(Guid PreSettlementGI, string Mode,string Remarks);
        public Task<DataSet> GetApprovalList_EXCEL(int Month, int PayrollGroupCode, Filter filter);

    }
}
