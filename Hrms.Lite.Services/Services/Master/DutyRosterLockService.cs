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
    public class DutyRosterLockService :IDutyRosterLockService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;

        private readonly IServiceBase _httpService;
        public DutyRosterLockService(IServiceBase httpService)
        {
            _baseURL = "DutyRosterLock";           
            _httpService = httpService;
        }
        public async Task<DutyRosterLock> GetList(int MonthCode)
        {
            return await _httpService.GetAsync<DutyRosterLock>($"{_baseURL}/GetList?MonthCode={MonthCode}");
        }
        public Task<ResponseEntity> Save(DutyRosterLock model)
        {
            return _httpService.PostAsync<ResponseEntity, DutyRosterLock>($"{_baseURL}/Save", model);
        }
        public Task<ResponseEntity> BachUpdate(DutyRosterLock model)
        {
            return _httpService.PostAsync<ResponseEntity, DutyRosterLock>($"{_baseURL}/BachUpdate", model);
        }
    }
}
