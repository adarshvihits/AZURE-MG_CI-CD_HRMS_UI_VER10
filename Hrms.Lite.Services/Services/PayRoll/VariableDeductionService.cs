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
   
    public class VariableDeductionService : IVariableDeductionService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public VariableDeductionService(IServiceBase httpService)
        {
            _baseURL = "VariableDeduction";
            _httpService = httpService;
        }


        public async Task<VariableDeduction> VariableDeductionList(int Month, int Deduction,Filter filter)
        {
            return await _httpService.PostAsync<VariableDeduction,Filter>($"{_baseURL}/VariableDeductionList?Month={Month}&Deduction={Deduction}",filter);
        }
        public async Task<ResponseEntity> VariableDeductionBatchUpdate(VariableDeduction model)
        {
            return await _httpService.PostAsync<ResponseEntity, VariableDeduction>($"{_baseURL}/VariableDeductionBatchUpdate", model);
        }

        public async Task<ResponseEntity> EmployeeWiseVariableDeductionSave(VariableDeduction model)
        {
            return await _httpService.PostAsync<ResponseEntity, VariableDeduction>($"{_baseURL}/EmployeeWiseVariableDeductionSave", model);
        }

        public async Task<DataSet> VariableDeductionList_Excel(int Month, int Deduction, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/VariableDeductionList_Excel?Month={Month}&Deduction={Deduction}", filter);
        }
        public async Task<ResponseEntity<List<VariableDeduction>>> ImportFromExcel(VariableDeduction model)
        {
            return await _httpService.PostAsync<ResponseEntity<List<VariableDeduction>>, VariableDeduction>($"{_baseURL}/ImportFromExcel", model);

        }
        //----------------------Variable Deduction Upload Month wise Only
        public async Task<VariableDeduction> GetVariableDeductionUploadList(int Month)
        {
            return await _httpService.GetAsync<VariableDeduction>($"{_baseURL}/VariableDeductionUploadList?Month={Month}");
        }
        public async Task<ResponseEntity<List<VariableDeduction>>> ImportFromExcelMultiple(VariableDeduction model)
        {
            return await _httpService.PostAsync<ResponseEntity<List<VariableDeduction>>, VariableDeduction>($"{_baseURL}/ImportFromExcelMultiple", model);

        }
    }
}
