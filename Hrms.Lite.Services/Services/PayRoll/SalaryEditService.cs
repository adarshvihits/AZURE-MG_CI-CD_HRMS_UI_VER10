using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.PayRoll
{
  public  class SalaryEditService: ISalaryEditService
    {

        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public SalaryEditService(IServiceBase httpService)
        {
            _baseURL = "SalaryEdit";
            _httpService = httpService;
        }
        public async Task<SalaryEdit> GetList(int Month, int PayrollGroupCode, Filter filter)
        {
            return await _httpService.PostAsync<SalaryEdit, Filter>($"{_baseURL}/GetSalaryEditList?Month={Month}&PayrollGroupCode={PayrollGroupCode}", filter);
        }
        public async Task<SalaryEdit> GetSalaryEditDetails(int Month, Guid EmployeeGI)
        {
            return await _httpService.GetAsync<SalaryEdit>($"{_baseURL}/GetSalaryEditDetails?EmployeeGI={EmployeeGI}&Month={Month}");
        }
        public async Task<ResponseEntity> SalaryEditSave(SalaryEdit model)
        {
            return await _httpService.PostAsync<ResponseEntity, SalaryEdit>($"{_baseURL}/SalaryEditSave", model);
        }
    }
}
