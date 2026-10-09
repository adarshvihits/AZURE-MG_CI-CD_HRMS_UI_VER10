using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TimeOffice.Report;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.TimeOffice.Report
{

   public class CoffBalanceRptService: ICoffBalanceRptService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public CoffBalanceRptService(IServiceBase httpService)
        {
            _baseURL = "CoffBalanceRpt";
            _httpService = httpService;
        }
        public async Task<CoffBalanceReport> GetList(Filter filter)
        {
            return await _httpService.PostAsync<CoffBalanceReport, Filter>($"{_baseURL}/GetReport", filter);
        }
        public async Task<CoffBalanceReport> GetCoffBalDetails(Guid empGI)
        {
            return await _httpService.GetAsync<CoffBalanceReport>($"{_baseURL}/GetCoffBalDetails?EmployeeGI={empGI}");
        }
        public async Task<CoffBalance> GetCoffBalanceDetails(Guid EmployeeGI, string TYPE)
        {
            return await _httpService.GetAsync<CoffBalance>($"{_baseURL}/GetCoffBalanceDetails?EmployeeGI={EmployeeGI}&TYPE={TYPE}");
        }
        public async Task<CoffBalance> GetCoffLeaveBalance(Guid EmployeeGI)
        {
            return await _httpService.GetAsync<CoffBalance>($"{_baseURL}/GetCoffLeaveBalance?EmployeeGI={EmployeeGI}");
        }
        public async Task<DataSet> CoffBalanceReport_Excel(Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetReportExcel", filter);
        }
    }
}
