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

    public class RecruitmentSettingsService : IRecruitmentSettingsService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public RecruitmentSettingsService(IServiceBase httpService)
        {
            _baseURL = "RecruitmentSettings";
            _httpService = httpService;
        }
        public async Task<RecruitmentSettings> GetList()
        {
            return await _httpService.GetAsync<RecruitmentSettings>($"{_baseURL}/GetRecruitmentSettingsList");
        }
        public async Task<RecruitmentSettings> GetDetailsList(Guid DesignationGI)
        {
            return await _httpService.GetAsync<RecruitmentSettings>($"{_baseURL}/GetRecruitmentSettingsDetails?DesignationGI={DesignationGI}");
        }
        public Task<ResponseEntity> Create(RecruitmentSettings input)
        {
            return _httpService.PostAsync<ResponseEntity, RecruitmentSettings>($"{_baseURL}/Cancel", input);
        }
    }
}
