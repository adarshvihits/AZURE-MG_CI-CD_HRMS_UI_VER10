using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
    public class EmployeeVsCostCenterService: IEmployeeVsCostCenterService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public EmployeeVsCostCenterService(IServiceBase httpService)
        {
            _baseURL = "EmployeeVsCostCenter";
            _httpService = httpService;
        }
        public async Task<CostCenter> GetList(string Tabindex, Filter filter)
        {
            return await _httpService.PostAsync<CostCenter, Filter>($"{_baseURL}/List?Tabindex={Tabindex}", filter);
        }
        public async Task<ResponseEntity> Create(CostCenter model)
        {
            return await _httpService.PostAsync<ResponseEntity, CostCenter>($"{_baseURL}/Create", model);
        }
        public async Task<ResponseEntity> BatchUpdate(CostCenter model)
        {
            return await _httpService.PostAsync<ResponseEntity, CostCenter>($"{_baseURL}/BatchUpdate", model);
        }
        public async Task<DataSet> EmployeeCostCenter_Excel(string TabIndex,Filter Filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetEmployeeCostCenter_Excel?TabIndex={TabIndex}", Filter);
        }
        public async Task<ResponseEntity<List<CostCenter>>> ImportFromExcel(string TabIndex,CostCenter model)
        {
            return await _httpService.PostAsync<ResponseEntity<List<CostCenter>>, CostCenter>($"{_baseURL}/ImportFromExcel?TabIndex={TabIndex}", model);

        }
    }
}
