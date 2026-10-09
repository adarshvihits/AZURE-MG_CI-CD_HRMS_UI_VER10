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
    public class LoanClosureService : ILoanClosureService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public LoanClosureService(IServiceBase httpService)
        {
            _baseURL = "LoanClosure";
            _httpService = httpService;
        }
        async Task<LoanIssue> ILoanClosureService.GetLoanClosureList(int LoanType,Filter filter)
        {
            return await _httpService.PostAsync<LoanIssue,Filter>($"{_baseURL}/GetLoanClosureList?LoanType={LoanType}",filter);
        }
        async Task<LoanClosureDetails> ILoanClosureService.GetLoanClosureDetails(Guid LoanIssueGI)
        {
            return await _httpService.GetAsync<LoanClosureDetails>($"{_baseURL}/GetLoanClosureDetails?LoanIssueGI={LoanIssueGI}");
        }
        async Task<ResponseEntity> ILoanClosureService.LoanClosureSave(LoanClosureDetails model)
        {
            return await _httpService.PostAsync<ResponseEntity, LoanClosureDetails>($"{_baseURL}/LoanClosureSave", model);
        }
    }
}
