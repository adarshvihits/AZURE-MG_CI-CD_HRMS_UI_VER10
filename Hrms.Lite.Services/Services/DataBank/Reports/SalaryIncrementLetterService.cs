using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank.Reports
{
    public class SalaryIncrementLetterService : ISalaryIncrementLetterService
    {

        private readonly string _baseURL;

        private readonly IServiceBase _httpService;
        public SalaryIncrementLetterService(IServiceBase httpService)
        {

            _baseURL = "Common";
            _httpService = httpService;
        }

        public async Task<Url> GetOfferLetter(string Type)
        {
            return await _httpService.GetAsync<Url>($"{_baseURL}/GetOfferLetter?Type={Type}");
        }

        public async Task<Url> GetUrl()
        {
            return await _httpService.GetAsync<Url>($"{_baseURL}/GetAPIUrl");
        }
    }
}