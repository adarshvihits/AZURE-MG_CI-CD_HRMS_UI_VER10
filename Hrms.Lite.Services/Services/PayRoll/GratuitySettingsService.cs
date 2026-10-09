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
    public class GratuitySettingsService: IGratuitySettingsService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public GratuitySettingsService(IServiceBase httpService)
        {
            _baseURL = "GratuitySettings";
            _httpService = httpService;
        }
        public async Task<GratuitySettings> GetGratuitySettingsDetails()
        {
            return await _httpService.GetAsync<GratuitySettings>($"{_baseURL}/GetGratuitySettingsDetails");
        }
        public async Task<ResponseEntity> SaveGratuitySettings(GratuitySettings model)
        {
            return await _httpService.PostAsync<ResponseEntity, GratuitySettings>($"{_baseURL}/SaveGratuitySettings", model);
        }
    }
}
