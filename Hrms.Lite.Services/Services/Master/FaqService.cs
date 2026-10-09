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
  public  class FaqService:IFaqService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public FaqService(IServiceBase httpService)
        {
            _baseURL = "Faq";
            _baseURL1 = "Common";
            _httpService = httpService;
        }
        public async Task<Faq> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<Faq>($"{_baseURL}/List?TabIndex={TabIndex}");
        }
        public Task<ResponseEntity> Create(Faq input)
        {
            return _httpService.PostAsync<ResponseEntity, Faq>($"{_baseURL}/Create", input);
        }
        public Task<Faq> GetFaqDetails(Guid FaqGI, Guid LogGI, string type)
        {
            return _httpService.GetAsync<Faq>($"{_baseURL}/FaqDetails?FaqGI={FaqGI}&LogGI={LogGI}&Type={type}");
        }

        public async Task<ResponseEntity> Delete(Faq model)
        {
            return await _httpService.PostAsync<ResponseEntity, Faq>($"{_baseURL}/Delete", model);

        }

        public Task<ResponseEntity> ApproveOrReject(Faq model)
        {
            return _httpService.PostAsync<ResponseEntity, Faq>($"{_baseURL}/ApproveOrReject", model);
        }
    }
}
