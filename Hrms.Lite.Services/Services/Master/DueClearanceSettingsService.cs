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
    public class DueClearanceSettingsService: IDueClearanceSettingsService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public DueClearanceSettingsService(IServiceBase httpService)
        {
            _baseURL = "DueClearanceSettings";
            _httpService = httpService;
        }
        public async Task<DueClearanceSettings> GetDueClearanceSettingsList()
        
        {
            return await _httpService.GetAsync<DueClearanceSettings>($"{_baseURL}/GetDueClearanceSettingsList");
        }
        public Task<ResponseEntity> Create(DueClearanceSettings model)
        {
            return _httpService.PostAsync<ResponseEntity, DueClearanceSettings>($"{_baseURL}/Save", model);
        }
        public async Task<DueClearanceSettings> GetDueClearanceSettingsDetails(Guid ParaGI)
        {
            return await _httpService.GetAsync<DueClearanceSettings>($"{_baseURL}/GetDueClearanceSettingsDetails?ParameterGI={ParaGI}");
        }
        public async Task<ResponseEntity> Delete(Guid ParaGI)
        {
            return await _httpService.DeleteAsync<ResponseEntity>($"{_baseURL}/Delete?ParameterGI={ParaGI}");
        }
    }
}
