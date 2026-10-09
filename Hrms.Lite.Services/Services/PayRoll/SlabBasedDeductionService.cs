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
    public class SlabBasedDeductionService : ISlabBasedDeductionService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public SlabBasedDeductionService(IServiceBase httpService)
        {
            _baseURL = "SlabBasedDeductionSettings";
            _httpService = httpService;
        }
        public async  Task<SlabDeductionSettings> GetList(int DeductionCode)
        {
            return await _httpService.GetAsync<SlabDeductionSettings>($"{_baseURL}/List?DeductionCode={DeductionCode}");

        }

        public async Task<ResponseEntity> Save(SlabDeductionSettings model)
        {
            return await _httpService.PostAsync<ResponseEntity, SlabDeductionSettings>($"{_baseURL}/Save", model);
        }
    }
}
