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
   public class QuoteOfDayServices : IQuoteOfDayServices
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public QuoteOfDayServices(IServiceBase httpService)
        {
            _baseURL = "QuotesOfDay";
            _httpService = httpService;
        }
        public async Task<QuotesOfDay> GetList(int FYCODE)
        {
            return await _httpService.GetAsync<QuotesOfDay>($"{_baseURL}/List?FYCODE={FYCODE}");
        }
     
        public Task<ResponseEntity> QuotesOfDaySave(QuotesOfDay input)
        {
            return _httpService.PostAsync<ResponseEntity, QuotesOfDay>($"{_baseURL}/QuotesOfDaySave", input);
        }
        public Task<ResponseEntity> Delete(QuotesOfDay model)
        {
            return _httpService.PostAsync<ResponseEntity, QuotesOfDay>($"{_baseURL}/Delete", model);
        }
        public async Task<QuotesOfDay> GetBatchUpdateDetails(QuotesOfDay model)
        {
            return await _httpService.GetAsync<QuotesOfDay>($"{_baseURL}/GetBatchUpdateDetails?");
        }
        public async Task<ResponseEntity> BatchUpdateSave(QuotesOfDay model)
        {
            return await _httpService.PostAsync<ResponseEntity, QuotesOfDay>($"{_baseURL}/BatchUpdateSettings", model);
        }
    }
}
