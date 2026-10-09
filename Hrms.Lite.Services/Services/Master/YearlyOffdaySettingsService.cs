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
    public class YearlyOffdaySettingsService: IYearlyOffdaySettingsService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public YearlyOffdaySettingsService(IServiceBase httpService)
        {
            _baseURL = "YearlyOffdaySettings";
            _httpService = httpService;
        }

        public async Task<ResponseEntity> Delete(YearlyOffdaySettings model)
        {
            return await _httpService.PostAsync<ResponseEntity, YearlyOffdaySettings>($"{_baseURL}/Delete", model);
        }

        public async Task<YearlyOffdaySettings> GetDaysList(int Year, YearlyOffdaySettings model)
        {
            return await _httpService.PostAsync<YearlyOffdaySettings, YearlyOffdaySettings>($"{_baseURL}/GetOffDay_Dates?Year={Year}",model);
        }


        public async Task<YearlyOffdaySettings> GetDetails(Guid OffdaySettingsGI)
        {
            return await _httpService.GetAsync<YearlyOffdaySettings>($"{_baseURL}/GetDetails?OffdaySettingsGI={OffdaySettingsGI}");
        }

        public async Task<YearlyOffdaySettings> GetYearlyOffdayList()
        {
            return await _httpService.GetAsync<YearlyOffdaySettings>($"{_baseURL}/GetList");
        }

        public async Task<ResponseEntity> Save(YearlyOffdaySettings model)
        {
            return await _httpService.PostAsync<ResponseEntity, YearlyOffdaySettings>($"{_baseURL}/Save",model);
        }

     
    }
}
