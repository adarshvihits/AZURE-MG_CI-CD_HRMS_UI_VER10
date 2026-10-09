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
    public class SalaryAdvanceIssueService : ISalaryAdvanceIssueService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public SalaryAdvanceIssueService(IServiceBase httpService)
        {
            _baseURL = "SalaryAdvIssue";
            _httpService = httpService;
        }
        public async Task<SalaryAdvance> GetSalaryAdvIssueList(int LoanType, int Month, int PayrollGroupCode, Filter filter)
        {
            return await _httpService.PostAsync<SalaryAdvance, Filter>($"{_baseURL}/GetSalaryAdvIssueList?LoanType={LoanType}&Month={Month}&PayrollGroupCode={PayrollGroupCode}", filter);
        }

        public async Task<SalaryAdvance> GetPendingApprovalList(int Month, int PayrollGroupCode, Filter filter)
        {
            return await _httpService.PostAsync<SalaryAdvance, Filter>($"{_baseURL}/GetPendingApprovalList?Month={Month}&PayrollGroupCode={PayrollGroupCode}", filter);
        }
     
        public async Task<ResponseEntity> SalaryAdvIssueSave(SalaryAdvance model)
        {
            return await _httpService.PostAsync<ResponseEntity, SalaryAdvance>($"{_baseURL}/SalaryAdvIssueSave", model);
        }
   
        public async Task<SalaryAdvance> GetSalaryAdvIssueHistoryList(int LoanType, int Month, Filter filter)
        {
            return await _httpService.PostAsync<SalaryAdvance, Filter>($"{_baseURL}/GetSalaryAdvIssueHistoryList?LoanType={LoanType}&Month={Month}", filter);
        }
        public async Task<SalaryAdvance> SalaryAdvIssueTracker(Guid RequestGI)
        {
            return await _httpService.GetAsync<SalaryAdvance>($"{_baseURL}/SalaryAdvIssueTracker?RequestGI={RequestGI}");
        }       
    }
}
