using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll.Reports;
using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.PayRoll.Reports
{
   public class AccountStatementService: IAccountStatementService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public AccountStatementService(IServiceBase httpService)
        {
            _baseURL = "BankStatement";
            _httpService = httpService;
        }
        public async Task<BankStatement> GetList(int Month)
        {
            return await _httpService.GetAsync<BankStatement>($"{_baseURL}/GetAccountStatement?SalaryPeriod={Month}");
        }

        public async Task<BankStatement> GetEmployeeList(int Month,string Type,string desc, int Code)
        {
            return await _httpService.GetAsync<BankStatement>($"{_baseURL}/GetAccountStatement_EmployeeList?SalaryPeriod={Month}&Type={Type}&Description={desc}&Code={Code}");

        }

        async Task<DataSet> IAccountStatementService.GetAccountStatement_EmployeeList_Excel(int SalaryPeriod, string Type, string Description, int Code)
        {
            return await _httpService.GetAsync<DataSet>($"{_baseURL}/GetAccountStatement_EmployeeList_Excel?SalaryPeriod={SalaryPeriod}&Type={Type}&Description={Description}&Code={Code}");

        }
    }
}
