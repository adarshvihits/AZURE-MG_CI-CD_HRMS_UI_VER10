using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll
{
    public interface ISalaryAdvanceIssueService
    {
        public Task<SalaryAdvance> GetSalaryAdvIssueList(int LoanType, int Month, int PayrollGroupCode, Filter filter);
        public Task<SalaryAdvance> GetPendingApprovalList(int Month, int PayrollGroupCode, Filter filter);
        public Task<ResponseEntity> SalaryAdvIssueSave(SalaryAdvance model);
        public Task<SalaryAdvance> GetSalaryAdvIssueHistoryList(int LoanType, int Month, Filter filter);
        Task<SalaryAdvance> SalaryAdvIssueTracker(Guid RequestGI);
    }
}
