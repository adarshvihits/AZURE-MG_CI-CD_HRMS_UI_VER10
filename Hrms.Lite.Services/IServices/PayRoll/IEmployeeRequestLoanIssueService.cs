using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
namespace Hrms.Lite.Services.IServices.PayRoll
{
 public interface IEmployeeRequestLoanIssueService
    {

        public Task<EmployeeRequestLoanIssue> GetLoanRequestList(int LoanType, int PayrollGroupCode, Filter filter);
        public Task<ResponseEntity> SaveLoanIssueData(EmployeeRequestLoanIssue model);
        public Task<EmployeeRequestLoanIssue> GetLoanIssueHistoryList(int LoanType, int Month, Filter filter);
        Task<EmployeeRequestLoanIssue> LoanIssueTracker(Guid RequestGI);


        public Task<EmployeeRequestLoanIssue> GetLoanRequestWFTrackingReport(int LoanType,  Filter filter);
    }
}
