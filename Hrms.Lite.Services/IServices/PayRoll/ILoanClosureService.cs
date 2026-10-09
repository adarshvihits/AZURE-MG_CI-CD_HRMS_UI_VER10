using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll
{
    public interface ILoanClosureService
    {
        public Task<LoanIssue> GetLoanClosureList(int LoanType,Filter filter);
        public Task<LoanClosureDetails> GetLoanClosureDetails(Guid LoanIssueGI);
        public Task<ResponseEntity> LoanClosureSave(LoanClosureDetails model);
    }
}
