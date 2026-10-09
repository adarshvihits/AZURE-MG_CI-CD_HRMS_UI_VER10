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
    public class LOPSettingsService : ILOPSettingsService

    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public LOPSettingsService(IServiceBase httpService)
        {
            _baseURL = "LOPSettings";
            _httpService = httpService;
        }
        public async Task<LOPSettings> GetLOPSettingsApplicableAllowanceList()
        {
            return await _httpService.GetAsync<LOPSettings>($"{_baseURL}/GetLOPSettingsApplicableAllowanceList");
        }

        public Task<ResponseEntity> Save(LOPSettings input)
        {
            return _httpService.PostAsync<ResponseEntity, LOPSettings>($"{_baseURL}/LOPSettingsSave", input);
        }
       
    }
}
