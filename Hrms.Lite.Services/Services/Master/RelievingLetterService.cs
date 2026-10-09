using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.Master
{
   public class RelievingLetterService:IRelievingLetterService
    {
        private readonly string _baseURL;
       
        private readonly IServiceBase _httpService;
        public RelievingLetterService(IServiceBase httpService)
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
