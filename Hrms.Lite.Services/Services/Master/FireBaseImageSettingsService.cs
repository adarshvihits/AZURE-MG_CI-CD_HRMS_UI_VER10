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
  public  class FireBaseImageSettingsService: IFireBaseImageSettingsService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public FireBaseImageSettingsService(IServiceBase httpService)
        {
            _baseURL = "FireBaseImageSettings";
            _httpService = httpService;
        }
        public async Task<FireBaseImageSettings> GetList()
        {
            return await _httpService.GetAsync<FireBaseImageSettings>($"{_baseURL}/GetList");
        }

        public Task<FireBaseImageSettings> GetDetails(int Code)
        {
            return _httpService.GetAsync<FireBaseImageSettings>($"{_baseURL}/GetDetails?Code={Code}");
        }
        public Task<ResponseEntity> Save(FireBaseImageSettings input)
        {
            return _httpService.PostAsync<ResponseEntity, FireBaseImageSettings>($"{_baseURL}/Create", input);
        }

        public async Task<ResponseEntity> MobileGeneralSettingsSave(FireBaseImageSettings model)
        {
            return await _httpService.PostAsync<ResponseEntity, FireBaseImageSettings>($"{_baseURL}/MobileGeneralSettingsSave", model);
        }
        public async Task<FireBaseImageSettings> GetMobileGeneralSettingsDetails(string TabIndex)
        {
            return await _httpService.GetAsync<FireBaseImageSettings>($"{_baseURL}/GetMobileGeneralSettingsDetails");

        }
    }
}
