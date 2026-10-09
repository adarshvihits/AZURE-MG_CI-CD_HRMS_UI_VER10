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
    public class StateVsLWFService : IStateVsLWFService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public StateVsLWFService(IServiceBase httpService)
        {
            _baseURL = "LWFSettings";
            _httpService = httpService;
        }
        public async Task<LWFSettings> GetList(int DeductionCode)
        {
            return await _httpService.GetAsync<LWFSettings>($"{_baseURL}/GetLWFSettingsList?DeductionCode={DeductionCode}");
        }

        public async Task<LWFSettings> GetPopUpDetails(int StateCode, int DeductionCode)
        {
            return await _httpService.GetAsync<LWFSettings>($"{_baseURL}/GetLWFStateDetails?StateCode={StateCode}&DeductionCode={DeductionCode}");
        }

        public async Task<ResponseEntity> Save(LWFSettings model)
        {
            return await _httpService.PostAsync<ResponseEntity,LWFSettings>($"{_baseURL}/LWFSettingsSave?",model);
        }
    }
}
