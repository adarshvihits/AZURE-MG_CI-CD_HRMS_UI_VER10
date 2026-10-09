using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
    public class AbscountingEmployeeTerminationService: IAbscountingEmployeeTerminationService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public AbscountingEmployeeTerminationService(IServiceBase httpService)
        {
            _baseURL = "AbscountingEmployeeTermination";
            _httpService = httpService;
        }
        public async Task<AbscondingReport> GetEmployeeList(Filter filter)
        {
            return await _httpService.PostAsync<AbscondingReport, Filter>($"{_baseURL}/GetAbscondingEmployeeList", filter);
        }
        public async Task<DataSet> GetAbscondingEmployeeList_Excel(Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetAbscondingEmployeeList_Excel", filter);
        }
        public async Task<AbscondingReport> GetAbscondingEmployeeDetails(Guid EmployeeGI)
        {
            return await _httpService.GetAsync<AbscondingReport>($"{_baseURL}/GetAbscondingEmployeeDetails?EmployeeGI={EmployeeGI}");
        }
        public async Task<AbscondingReport> GetAbscondingEmployeeSalaryDetails(Guid EmployeeGI,string LWD)
        {
            return await _httpService.GetAsync<AbscondingReport>($"{_baseURL}/GetAbscondingEmployeeSalaryDetails?EmployeeGI={EmployeeGI}&LWD={LWD}");
        }
        public async Task<ResponseEntity> Create(AbscondingReport input)
        {
            return await _httpService.PostAsync<ResponseEntity, AbscondingReport>($"{_baseURL}/AbscondingEmployeeTermination", input);
        }
    }
}
