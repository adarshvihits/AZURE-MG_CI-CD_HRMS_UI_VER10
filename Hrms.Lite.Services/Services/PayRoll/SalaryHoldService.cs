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
   public class SalaryHoldService: ISalaryHoldService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public SalaryHoldService(IServiceBase httpService)
        {
            _baseURL = "SalaryHold";
            _httpService = httpService;
        }

        public async Task<SalaryHold> GetList(int Month, int PayrollGroupCode,Filter filter)
        {
            return await _httpService.PostAsync<SalaryHold, Filter>($"{_baseURL}/GetSalaryHoldList?" + $"Month={Month}&PayrollGroupCode={PayrollGroupCode}", filter);

        }

        public async Task<SalaryHold> GetSalaryHoldDetails(Guid EmpGI, int month)
        {
            return await _httpService.GetAsync<SalaryHold>($"{_baseURL}/GetSalaryHoldDetails?EmployeeGI={EmpGI}&Month={month}");
        }
        public async Task<ResponseEntity> Save(SalaryHold model)
        {
            return await _httpService.PostAsync<ResponseEntity, SalaryHold>($"{_baseURL}/GetSalaryHoldSave", model);
        }
        public async Task<DataSet> GetSalaryHold_Excel(int Month, int PayrollGroupCode, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetExcelSalaryHold?Month=" +
                $"{Month}&PayrollGroupCode={PayrollGroupCode}", filter);
        }

        public async Task<ResponseEntity<List<SalaryHold>>> ImportFromExcel(SalaryHold model, int PayrollGroupCode)
        {
            return await _httpService.PostAsync<ResponseEntity<List<SalaryHold>>, SalaryHold>($"{_baseURL}/ImportFromExcel?PayrollGroupCode={PayrollGroupCode}", model);

        }


    }
}
