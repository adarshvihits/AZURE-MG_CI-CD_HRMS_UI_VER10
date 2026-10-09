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
    public class LoanTopUpService:ILoanTopUpService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public LoanTopUpService(IServiceBase httpService)
        {
            _baseURL = "LoanTopUp";
            _httpService = httpService;
        }
       
        public async Task<LoanIssue> GetLoanTopUpList(int loantype,Filter filter)
        {
            return await _httpService.PostAsync<LoanIssue,Filter>($"{_baseURL}/GetLoanTopUpList?loantype={loantype}", filter);
        }

        public async Task<LoanTopUpDetails> GetLoanTopUpDetails(Guid LoanIssueGI)
        {
            return await _httpService.GetAsync<LoanTopUpDetails>($"{_baseURL}/GetLoanTopUPDetails?LoanIssueGI={LoanIssueGI}");
        }
        async Task<ResponseEntity> ILoanTopUpService.SaveLoanTopUp(LoanTopUpDetails model)
        {
            return await _httpService.PostAsync<ResponseEntity, LoanTopUpDetails>($"{_baseURL}/LoanTopUpSave", model);
        }

    }
}
