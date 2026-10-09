using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll
{
    public interface ILoanIssueService
    {
        public Task<IList<LoanIssueListTabs>> GetLoanIssueListTabs();
        public Task<LoanIssue> GetLoanIssueList(int Tabindex, int? LoanSettingsCode,Filter filter);
        public Task<LoanIssue> GetLoanIssueDetails(Guid LoanIssueGI);
        public Task<Dropdown> GetRepaymentMethod(int LoanType);
        public Task<LoanIssue> GetLoanAmountDetails(int LoanType);
        public Task<ResponseEntity> NewRequest(LoanIssue model);
        public Task<IList<LoanIssueLog>> GetLoanIssueLogList(Guid EmployeeGI);
        public Task<IList<LoanIssue>> LoanIssueLogDetails(Guid EmployeeGI);
        public Task<ResponseEntity> LoanIssueSave(LoanIssue model);
        public Task<ResponseEntity> Approve(LoanIssue model);

        Task<ResponseEntity<List<LoanIssue>>> ImportFromExcel(LoanIssue model);
        public Task<LoanIssueRequest> GetEmployeeLoanIssueDetails(Guid RequestGI);


    }
}
