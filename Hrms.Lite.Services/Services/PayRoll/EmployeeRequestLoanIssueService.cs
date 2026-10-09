using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.PayRoll
{
    public class EmployeeRequestLoanIssueService : IEmployeeRequestLoanIssueService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public EmployeeRequestLoanIssueService(IServiceBase httpService)
        {
            _baseURL = "EmployeeRequestLoanIssue";
            _httpService = httpService;
        }
     
        public async Task<EmployeeRequestLoanIssue> GetLoanRequestList(int LoanType,int PayrollGroupCode, Filter filter)
        {
            return await _httpService.PostAsync<EmployeeRequestLoanIssue, Filter>($"{_baseURL}/GetLoanRequestList?LoanType={LoanType}&PayrollGroupCode={PayrollGroupCode}", filter);
        }
        public async Task<ResponseEntity> SaveLoanIssueData(EmployeeRequestLoanIssue model)
        {
            return await _httpService.PostAsync<ResponseEntity, EmployeeRequestLoanIssue>($"{_baseURL}/LoanIssueSave", model);
        }
        public async Task<EmployeeRequestLoanIssue> GetLoanIssueHistoryList(int LoanType, int Month, Filter filter)
        {
            return await _httpService.PostAsync<EmployeeRequestLoanIssue, Filter>($"{_baseURL}/GetLoanRequestIssueHistoryList?LoanType={LoanType}&Month={Month}", filter);
        }
             public async Task<EmployeeRequestLoanIssue> LoanIssueTracker(Guid RequestGI)
        {
            return await _httpService.GetAsync<EmployeeRequestLoanIssue>($"{_baseURL}/LoanIssueTracker?RequestGI={RequestGI}");
        }



        public async Task<EmployeeRequestLoanIssue> GetLoanRequestWFTrackingReport(int LoanType,  Filter filter)
        {
            return await _httpService.PostAsync<EmployeeRequestLoanIssue, Filter>($"{_baseURL}/GetLoanRequestWFTrackingReport?LoanType={LoanType}", filter);
        }
    }
}
