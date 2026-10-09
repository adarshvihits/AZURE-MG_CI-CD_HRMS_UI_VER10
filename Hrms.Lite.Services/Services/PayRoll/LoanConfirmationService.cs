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
   public  class LoanConfirmationService: ILoanConfirmationService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public LoanConfirmationService(IServiceBase httpService)
        {
            _baseURL = "LoanConfirmation";
            _httpService = httpService;
        }
        public async Task<LoanConfirmation> GetList(Filter filter,int month,int loanType,string type)
        {
            return await _httpService.PostAsync<LoanConfirmation,Filter>($"{_baseURL}/LoanConfirmationList?month={month}&loanType={loanType}&type={type}",filter);
        }
        async Task<LoanConfirmation> ILoanConfirmationService.GetLoanConfirmationDetails(Guid LoanIssueGI,Guid EmployeeGI)
        {
            return await _httpService.GetAsync<LoanConfirmation>($"{_baseURL}/GetLoanConfirmationDetails?LoanIssueGI={LoanIssueGI}&EmployeeGI={EmployeeGI}");

        }
        async Task<ResponseEntity> ILoanConfirmationService.LoanConfirmationSave(LoanConfirmation model)
        {
            return await _httpService.PostAsync<ResponseEntity, LoanConfirmation>($"{_baseURL}/LoanConfirmationSave", model);
        }
        async Task<ResponseEntity> ILoanConfirmationService.LoanConfirmationEmployeeWiseSave(LoanConfirmation model)
        {
            return await _httpService.PostAsync<ResponseEntity, LoanConfirmation>($"{_baseURL}/LoanConfirmationEmployeeWiseSave", model);
        }
    }
}
