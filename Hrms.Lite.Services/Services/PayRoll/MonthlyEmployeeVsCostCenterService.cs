using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.PayRoll
{
    public class MonthlyEmployeeVsCostCenterService:IMonthlyEmployeeVsCostCenterService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public MonthlyEmployeeVsCostCenterService(IServiceBase httpService)
        {
            _baseURL = "MonthlyEmployeeVsCostCenter";
            _httpService = httpService;
        }


        public async Task<DataSet> GetCostCenterEmployeeList(int MonthCode, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetCostCenterEmployeeList?MonthCode={MonthCode}", filter);

        }

        public Task<MonthlyEmployeeVsCostCenter> GetEmpVsCostCenterEditList(int MonthCode, Guid EmployeeGI)
        {
            return _httpService.GetAsync<MonthlyEmployeeVsCostCenter>($"{_baseURL}/GetEmpVsCostCenterEditList?MonthCode={MonthCode}&EmployeeGI={EmployeeGI}");
        }

        public Task<ResponseEntity> SaveMonthlyEmployeeVsCostCenter(MonthlyEmployeeVsCostCenter input)
        {
            return _httpService.PostAsync<ResponseEntity, MonthlyEmployeeVsCostCenter>($"{_baseURL}/SaveMonthlyEmployeeVsCostCenter", input);
        }

        public Task<ResponseEntity> BatchUpdateEmployeeVsCostCenter(int FromMonth)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL}/BatchUpdateEmployeeVsCostCenter?MonthCode={FromMonth}");
        }

        public async Task<DataSet> GetCostCenterEmployeeList_Excel(int MonthCode, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetCostCenterEmployeeList_Excel?MonthCode={MonthCode}", filter);

        }
    }
}
