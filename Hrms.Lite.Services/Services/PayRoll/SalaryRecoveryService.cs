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
  public  class SalaryRecoveryService:ISalaryRecoveryService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public SalaryRecoveryService(IServiceBase httpService)
        {
            _baseURL = "SalaryRecovery";
            _httpService = httpService;
        }
        public async Task<SalaryRecovery> GetList(int Month, int AllowanceCode, Filter filter)
        {
            return await _httpService.PostAsync<SalaryRecovery, Filter>($"{_baseURL}/GetSalaryRecoveryList?Month={Month}&AllowanceCode={AllowanceCode}", filter);
        }
        public async Task<ResponseEntity> Save(SalaryRecovery model)
        {
            return await _httpService.PostAsync<ResponseEntity, SalaryRecovery>($"{_baseURL}/GetSalaryRecoverySave", model);
        }
        public async Task<ResponseEntity<List<SalaryRecovery>>> ImportFromExcel(SalaryRecovery model)
        {
            return await _httpService.PostAsync<ResponseEntity<List<SalaryRecovery>>, SalaryRecovery>($"{_baseURL}/ImportFromExcel", model);

        }
        public async Task<DataSet> SalaryRecoveryList_Excel(int Month, int Allowance, Filter Filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/SalaryRecoveryList_Excel?Month={Month}&Allowance={Allowance}", Filter);
        }
    }
}
