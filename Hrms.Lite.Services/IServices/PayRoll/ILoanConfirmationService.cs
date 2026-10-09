using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Hrms.Lite.Services.IServices.PayRoll
{
  public   interface ILoanConfirmationService
    {
        public Task<LoanConfirmation> GetList(Filter filter,int month,int loanType, string type);
        public Task<LoanConfirmation> GetLoanConfirmationDetails(Guid LoanIssueGI,Guid EmployeeGI);
        public Task<ResponseEntity> LoanConfirmationSave(LoanConfirmation model);
        public Task<ResponseEntity> LoanConfirmationEmployeeWiseSave(LoanConfirmation model);

    }
}
