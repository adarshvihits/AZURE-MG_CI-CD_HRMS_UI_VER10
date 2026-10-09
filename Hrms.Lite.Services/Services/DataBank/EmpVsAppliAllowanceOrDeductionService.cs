
using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
   public class EmpVsAppliAllowanceOrDeductionService: IEmpVsAppliAllowanceOrDeductionService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public EmpVsAppliAllowanceOrDeductionService(IServiceBase httpService)
        {
          
              _baseURL = "EmpVsAppliAllowanceOrDeduction";
            _httpService = httpService;
        }

        public async Task<EmpVsAppliAllowanceOrDeduction> GetAllowanceOrDeductionList(int Type, int AllowOrDeduction, int BasedOn, Filter filter)
        {
            return await _httpService.PostAsync<EmpVsAppliAllowanceOrDeduction, Filter>($"{_baseURL}/GetAllowanceOrDeductionList?Type={Type}&AllowOrDeduction={AllowOrDeduction}&BasedOn={BasedOn}",filter);
        }
         async Task<ResponseEntity> IEmpVsAppliAllowanceOrDeductionService.EmpVsApplAllowOrDedSave(EmpVsAppliAllowanceOrDeduction model)
        {
            return await _httpService.PostAsync<ResponseEntity, EmpVsAppliAllowanceOrDeduction>($"{_baseURL}/EmpVsApplAllowOrDedSave", model);

        }
        public async Task<ResponseEntity<List<ApplAllowOrDedList>>> ImportFromExcel(EmpVsAppliAllowanceOrDeduction model)
        {
            return await _httpService.PostAsync<ResponseEntity<List<ApplAllowOrDedList>>, EmpVsAppliAllowanceOrDeduction>($"{_baseURL}/ImportFromExcel", model);

        }
    }
}
