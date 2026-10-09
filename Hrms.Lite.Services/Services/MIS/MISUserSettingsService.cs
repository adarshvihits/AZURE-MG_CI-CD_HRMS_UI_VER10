using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.MIS;
using Hrms.Lite.Shared.DashBoard;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.MIS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Hrms.Lite.Services.Services.MIS
{
    public class MISUserSettingsService : IMISUserSettingsService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public MISUserSettingsService(IServiceBase httpService)
        {
            _baseURL = "MISUserSettings";
            _httpService = httpService;
        }
        public async Task<MISUserSettings> GetList(Filter filter)
        {
            return await _httpService.PostAsync<MISUserSettings, Filter>($"{_baseURL}/list?TabIndex={filter.Tabindex}", filter);
        }
        public Task<ResponseEntity> Create(MISUserSettings input)
        {
            return _httpService.PostAsync<ResponseEntity, MISUserSettings>($"{_baseURL}/Create", input);
        }

        public async Task<ResponseEntity> Delete(MISUserSettings model)
        {
            return await _httpService.PostAsync<ResponseEntity, MISUserSettings>($"{_baseURL}/Delete", model);

        }

    }
}
