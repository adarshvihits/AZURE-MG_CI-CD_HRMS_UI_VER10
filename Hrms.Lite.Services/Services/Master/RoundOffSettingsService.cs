using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
namespace Hrms.Lite.Services.Services.Master
{
   public class RoundOffSettingsService:IRoundOffSettingsService
    {

        private readonly string _baseURL;
        private readonly string _baseURL1;

        private readonly IServiceBase _httpService;
        public RoundOffSettingsService(IServiceBase httpService)
        {
            _baseURL = "RoundOffSettings";
            _baseURL1 = "Common";
            _httpService = httpService;
        }

        public async Task<RoundOffSettings> GetRoundOffSettings(string TabIndex)
        {
            return await _httpService.GetAsync<RoundOffSettings>($"{_baseURL}/GetRoundOffSettings?TabIndex={TabIndex}");
        }
        public async Task<ResponseEntity> Create(RoundOffSettings model)
        {
            return await _httpService.PostAsync<ResponseEntity, RoundOffSettings>($"{_baseURL}/SaveRoundOffSettings", model);
        }
        public async Task<DataSet> GetExcel(string TabIndex)
        {
            return await _httpService.GetAsync<DataSet>($"{_baseURL}/GetAccountHead_Excel?TabIndex={TabIndex}");
        }

     
    }
}

