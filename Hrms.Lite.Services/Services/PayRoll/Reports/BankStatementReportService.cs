using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll.Reports;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.PayRoll.Reports
{
 
    public class BankStatementReportService:IBankStatementReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public BankStatementReportService(IServiceBase httpService)
        {
            _baseURL = "BankStatement";
            _httpService = httpService;
        }

        public async Task<DataSet> GetBankStatement_Excel(BankStatement model)
        {
            return await _httpService.PostAsync<DataSet, BankStatement>($"{_baseURL}/GetBankStatement_Excel",model);

        }

        public async Task<BankStatement> GetList(BankStatement model)
        {
     
            return await _httpService.PostAsync<BankStatement, BankStatement>($"{_baseURL}/GetBankStatement",model);

        }
        public async Task<ResponseEntity<BankStatement>> AccountNumberDuplication(BankStatement model)
        {
            return await _httpService.PostAsync<ResponseEntity<BankStatement>, BankStatement>($"{_baseURL}/AccountNumberDuplication", model);

        }


        //public async Task<BankStatement> GetList(int MonthCode, int BankCode, Filter filter)
        //{
        //    return await _httpService.PostAsync<BankStatement,Filter>($"{_baseURL}/GetBankStatement?SalaryPeriod={MonthCode}&BankCode={BankCode}",filter);
        //}
    }
}
