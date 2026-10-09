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
    public class VariableAllowanceService : IVariableAllowanceService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public VariableAllowanceService(IServiceBase httpService)
        {
            _baseURL = "VariableAllowance";
            _httpService = httpService;
        }

        public async Task<ResponseEntity> EmployeeWiseVariableAllowanceSave(VariableAllowance model)
        {
            return await _httpService.PostAsync<ResponseEntity, VariableAllowance>($"{_baseURL}/EmployeeWiseVariableAllowanceSave", model);
        }

        public async Task<VariableAllowance> GetVariableAllowanceList(int Month, int Allowance,Filter Filter)
        {
            return await _httpService.PostAsync<VariableAllowance,Filter>($"{_baseURL}/VariableAllowanceList?Month={Month}&Allowance={Allowance}",Filter);
        }

        public async Task<ResponseEntity> VariableAllowanceBatchUpdate(VariableAllowance model)
        {
            return await _httpService.PostAsync<ResponseEntity, VariableAllowance>($"{_baseURL}/VariableAllowanceBatchUpdate", model);
        }
        public async Task<DataSet> VariableAllowanceList_Excel(int Month, int Allowance, Filter Filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/VariableAllowanceList_Excel?Month={Month}&Allowance={Allowance}", Filter);
        }
        public async Task<ResponseEntity<List<VariableAllowance>>> ImportFromExcel(VariableAllowance model)
        {
            return await _httpService.PostAsync<ResponseEntity<List<VariableAllowance>>, VariableAllowance>($"{_baseURL}/ImportFromExcel", model);

        }
        //----------------------Variable Allowance Upload Month wise Only
        public async Task<VariableAllowance> GetVariableAllowanceUploadList(int Month)
        {
            return await _httpService.GetAsync<VariableAllowance>($"{_baseURL}/VariableAllowanceUploadList?Month={Month}");
        }
        public async Task<ResponseEntity<List<VariableAllowance>>> ImportFromExcelMultiple(VariableAllowance model)
        {
            return await _httpService.PostAsync<ResponseEntity<List<VariableAllowance>>, VariableAllowance>($"{_baseURL}/ImportFromExcelMultiple", model);

        }
        ///---------WorkFlowConfirmation
        public async Task<WorkFlowConfirmation> ConfirmationList_GEO(string Tab, Filter filter, int Month, int AllowanceCode)
        {
            return await _httpService.PostAsync<WorkFlowConfirmation, Filter>($"{_baseURL}/ConfirmationList_GEO?Tab={Tab}&Month={Month}&AllowanceCode={AllowanceCode}", filter);

        }
        public async Task<ResponseEntity> EditUpdate(WorkFlowConfirmation Model)
        {
            return await _httpService.PostAsync<ResponseEntity, WorkFlowConfirmation>($"{_baseURL}/EditUpdate", Model);

        }

        public async Task<ResponseEntity> ConfirmWF(Filter filter, int Month, int AllowanceCode)
        {
            return await _httpService.PostAsync<ResponseEntity, Filter>($"{_baseURL}/ConfirmWF?Month={Month}&AllowanceCode={AllowanceCode}", filter);

        }
        public async Task<DataSet> GetConfirmationList_Excel(string Tab, Filter filter, int Month, int AllowanceCode)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetConfirmationList_Excel?Tab={Tab}&Month={Month}&AllowanceCode={AllowanceCode}", filter);

        }
    }
}
