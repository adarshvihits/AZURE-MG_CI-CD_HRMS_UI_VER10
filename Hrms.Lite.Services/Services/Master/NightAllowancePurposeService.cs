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
    public class NightAllowancePurposeService: INightAllowancePurposeService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public NightAllowancePurposeService(IServiceBase httpService)
        {
            _baseURL = "NightAllowancePurpose";
            _httpService = httpService;
        }
        public async Task<NightAllowancePurpose> GetList()
        {
            return await _httpService.GetAsync<NightAllowancePurpose>($"{_baseURL}/GetList");
        }
        public async Task<NightAllowancePurpose> GetDetails(Guid purposeGi)
        {
            return await _httpService.GetAsync<NightAllowancePurpose>($"{_baseURL}/GetDetails?purposeGi={purposeGi}");
        }
        public async Task<ResponseEntity> Save(NightAllowancePurpose model)
        {
            return await _httpService.PostAsync<ResponseEntity, NightAllowancePurpose>($"{_baseURL}/Save", model);
        }
        public Task<ResponseEntity> Delete(NightAllowancePurpose model)
        {
            return _httpService.PostAsync<ResponseEntity, NightAllowancePurpose>($"{_baseURL}/Delete", model);
        }
    }
}
