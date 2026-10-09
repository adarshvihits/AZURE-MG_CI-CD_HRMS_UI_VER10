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
    public class IncomeTaxRatesService : IIncomeTaxRatesService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public IncomeTaxRatesService(IServiceBase httpService)
        {
            _baseURL = "IncomeTaxRates";
            _httpService = httpService;
        }
        public async Task<IncomeTaxRates> IncomeTaxRatesList(int AssessmentYear, int ITCategory, int BasedOn)
        {
            return await _httpService.GetAsync<IncomeTaxRates>($"{_baseURL}/IncomeTaxRatesList?AssessmentYear={AssessmentYear}&ITCategory={ITCategory}&BasedOn={BasedOn}");
        }
        public async Task<ResponseEntity> IncomeTaxRates_Save(IncomeTaxRates model)
        {
            return await _httpService.PostAsync<ResponseEntity, IncomeTaxRates>($"{_baseURL}/IncomeTaxRates_Save", model);
        }
    }
}
