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
    public class CompanyGeneralSettingsService: ICompanyGeneralSettingsService
    {
        private readonly string _baseURL;
  

        private readonly IServiceBase _httpService;
        public CompanyGeneralSettingsService(IServiceBase httpService)
        {
            _baseURL = "CompanyGeneralSettings";
            _httpService = httpService;
        }


        public async Task<ResponseEntity> CompanyGeneralSettingsSave(CompanyGeneralSettings model)
        {
            return await _httpService.PostAsync<ResponseEntity, CompanyGeneralSettings>($"{_baseURL}/CompanyGeneralSettingsSave", model);
        }

       


        public async Task<CompanyGeneralSettings> GetCompanyGeneralSettingsDetails( )
        {
            return await _httpService.GetAsync<CompanyGeneralSettings>($"{_baseURL}/GetCompanyGeneralSettingsDetails");

        }
    }
}
