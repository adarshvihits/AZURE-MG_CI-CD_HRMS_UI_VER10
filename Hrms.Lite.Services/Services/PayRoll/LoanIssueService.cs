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
    public class LoanIssueService :  ILoanIssueService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public LoanIssueService(IServiceBase httpService)
        {
            _baseURL = "LoanIssue";
            _httpService = httpService;
        }
       public async Task<Dropdown> GetRepaymentMethod(int LoanType)
        {
            return await _httpService.GetAsync<Dropdown>($"{_baseURL}/GetRepaymentMethod?LoanType={LoanType}");
        }
      
        async Task<IList<LoanIssueListTabs>> ILoanIssueService.GetLoanIssueListTabs()
        {
            return await _httpService.GetAsync<IList<LoanIssueListTabs>>($"{_baseURL}/GetLoanIssueListTabs");
        }
        async Task<LoanIssue> ILoanIssueService.GetLoanIssueList(int Tabindex, int? LoanSettingsCode,Filter Filter)
        {
            return await _httpService.PostAsync<LoanIssue,Filter>($"{_baseURL}/GetLoanIssueList?Tabindex={Tabindex}&LoanSettingsCode={LoanSettingsCode}",Filter);
        }

        async Task<LoanIssue> ILoanIssueService.GetLoanIssueDetails(Guid LoanIssueGI)
        {
            return await _httpService.GetAsync<LoanIssue>($"{_baseURL}/GetLoanIssueDetails?LoanIssueGI={LoanIssueGI}");
        }
        async Task<LoanIssue> ILoanIssueService.GetLoanAmountDetails(int LoanType)
        {
            return await _httpService.GetAsync<LoanIssue>($"{_baseURL}/GetLoanAmountDetails?LoanType={LoanType}");
        }
        public async Task<ResponseEntity> NewRequest(LoanIssue model)
        {
            return await _httpService.PostAsync<ResponseEntity, LoanIssue>($"{_baseURL}/LoanIssueSave", model);
        }
        public async Task<IList<LoanIssueLog>> GetLoanIssueLogList(Guid EmployeeGI)
        {
            return await _httpService.GetAsync<IList<LoanIssueLog>>($"{_baseURL}/GetLoanIssueLogList?EmployeeGI={EmployeeGI}");

        }
        //public async Task<IList<LoanIssue>> LoanIssueLogDetails(Guid LoanIssueGI)
        //{
        //    return await _httpService.GetAsync<IList<LoanIssue>>($"{_baseURL}/GetLoanIssueDetails?LoanIssueGI={LoanIssueGI}");

        //}
        public async Task<IList<LoanIssue>> LoanIssueLogDetails(Guid EmployeeGI)
        {
            return await _httpService.GetAsync<IList<LoanIssue>>($"{_baseURL}/GetLoanIssueLogList?EmployeeGI={EmployeeGI}");

        }
        async Task<ResponseEntity> ILoanIssueService.LoanIssueSave(LoanIssue model)
        {
            return await _httpService.PostAsync<ResponseEntity, LoanIssue>($"{_baseURL}/LoanIssueSave", model);
        }

        async Task<ResponseEntity> ILoanIssueService.Approve(LoanIssue model)
        {
            return await _httpService.PostAsync<ResponseEntity,LoanIssue>($"{_baseURL}/LoanIssueApproveReject?LoanIssueGI",model);
        }

        async Task<ResponseEntity<List<LoanIssue>>> ILoanIssueService.ImportFromExcel(LoanIssue model)
        {
            return await _httpService.PostAsync<ResponseEntity<List<LoanIssue>>, LoanIssue>($"{_baseURL}/ImportFromExcel", model);
        }
        public async Task<LoanIssueRequest> GetEmployeeLoanIssueDetails(Guid RequestGI)
        {
            return await _httpService.GetAsync<LoanIssueRequest>($"{_baseURL}/GetEmployeeLoanIssueDetails?RequestGI={RequestGI}");

        }
    }
}
