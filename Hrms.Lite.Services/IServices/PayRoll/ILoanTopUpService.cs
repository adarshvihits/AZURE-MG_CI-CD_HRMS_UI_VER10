using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll
{
    public interface ILoanTopUpService
    {
        public Task<LoanIssue> GetLoanTopUpList(int loantype,Filter filter);
        public Task<LoanTopUpDetails> GetLoanTopUpDetails(Guid LoanIssueGI);
        public Task<ResponseEntity> SaveLoanTopUp(LoanTopUpDetails model);
    }
}
