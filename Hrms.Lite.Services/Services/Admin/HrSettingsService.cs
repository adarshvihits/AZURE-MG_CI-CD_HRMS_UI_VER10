using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Admin;
using Hrms.Lite.Shared.Admin;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.Admin
{
    public class HrSettingsService : IHrSettingsService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public HrSettingsService(IServiceBase httpService)
        {
            _baseURL = "HrSettings";
            _baseURL1 = "Common";
            _httpService = httpService;

        }

        public async Task<HrSettings> GetList()
        {
            return await _httpService.GetAsync<HrSettings>($"{_baseURL}/List");
        }

        public async Task<ResponseEntity> Save(HrSettings model)
        {
            return await _httpService.PostAsync<ResponseEntity, HrSettings>($"{_baseURL}/Save", model);
        }
    }
}
