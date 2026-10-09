using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.Master
{
   
    public  class SalaryPeriodLockService:ISalaryPeriodLockService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public SalaryPeriodLockService(IServiceBase httpService)
        {
            _baseURL = "SalaryPeriodLock";
            _httpService = httpService;
        }

        public async Task<ResponseEntity> Create(SalaryPeriodLock input)
        {
            return await _httpService.PostAsync<ResponseEntity,SalaryPeriodLock>($"{_baseURL}/Save",input);
        }

        public async Task<SalaryPeriodLock> GetList(int Month)
        {
            return await _httpService.GetAsync<SalaryPeriodLock>($"{_baseURL}/GetList?MonthCode={Month}");
        }

        public async Task<SalaryPeriodLock> GetOpenMonth(int Month, int PyrollGroup)
        {
            return await _httpService.GetAsync<SalaryPeriodLock>($"{_baseURL}/OpenMonth?MonthCode={Month}&&PGCode={PyrollGroup}");
        }

      public async Task<SalaryPeriodLock> GetLockUnLockLog(int MonthCode, int PGCode)
        {
            return await _httpService.GetAsync<SalaryPeriodLock>($"{_baseURL}/GetLockUnLockLog?MonthCode={MonthCode}&&PGCode={PGCode}");
        }
    }
}
