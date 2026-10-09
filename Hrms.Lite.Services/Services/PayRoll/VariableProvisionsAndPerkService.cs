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
    public class VariableProvisionsAndPerkService: IVariableProvisionsAndPerkService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public VariableProvisionsAndPerkService(IServiceBase httpService)
        {
            _baseURL = "VariableProvisionsAndPerk";
            _httpService = httpService;
        }
        public async Task<VariableAllowance> GetVariableProvisionList(int Month, int Allowance, Filter Filter)
        {
            return await _httpService.PostAsync<VariableAllowance, Filter>($"{_baseURL}/VariableProvisionsAndPerkList?Month={Month}&Allowance={Allowance}", Filter);
        }
        public async Task<ResponseEntity> EmployeeWiseUpdate(VariableAllowance model)
        {
            return await _httpService.PostAsync<ResponseEntity, VariableAllowance>($"{_baseURL}/EmployeeWiseUpdate", model);
        }
        public async Task<ResponseEntity> VariableProvisionsBatchUpdate(VariableAllowance model)
        {
            return await _httpService.PostAsync<ResponseEntity, VariableAllowance>($"{_baseURL}/VariableProvisionsBatchUpdate", model);
        }
        public async Task<DataSet> VariableAllowanceList_Excel(int Month, int Allowance, Filter Filter,string Type)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/VariableProvisionsList_Excel?Month={Month}&Allowance={Allowance}&Type={Type}", Filter);
        }
        public async Task<ResponseEntity<List<VariableAllowance>>> ImportFromExcel(VariableAllowance model)
        {
            return await _httpService.PostAsync<ResponseEntity<List<VariableAllowance>>, VariableAllowance>($"{_baseURL}/ImportFromExcel", model);

        }

    }
}
