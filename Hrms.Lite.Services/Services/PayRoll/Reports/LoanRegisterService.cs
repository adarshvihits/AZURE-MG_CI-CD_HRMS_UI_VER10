using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll.Reports;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.PayRoll.Reports
{
    public class LoanRegisterService: ILoanRegisterService
    {

        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public LoanRegisterService(IServiceBase httpService)
        {
            _baseURL = "LoanRegister";
            _httpService = httpService;
        }
        public async Task<LoanIssue> GetLoanRegisterList(int Tab, int type, Filter filter)
        {
            return await _httpService.PostAsync<LoanIssue, Filter>($"{_baseURL}/GetLoanRegister?LoanType={type}&TabIndex={Tab}", filter);
        }
        public async Task<LoanIssue> GetLoanRegisterDetails(Guid EmployeeGI, int LoanIssueCode)
        {
            return await _httpService.GetAsync<LoanIssue>($"{_baseURL}/GetLoanDetails?EmployeeGI={EmployeeGI}&LoanIssueCode={LoanIssueCode}");

        }

       public async Task<DataSet> GetLoanRegister_Excel(int Tab, int type, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetLoanRegister_Excel?LoanType={type}&TabIndex={Tab}", filter);

        }
    }
}
