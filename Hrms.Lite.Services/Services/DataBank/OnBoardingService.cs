using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
    public class OnBoardingService : IOnBoardingService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public OnBoardingService(IServiceBase httpService)
        {
            _baseURL = "OnBoarding";
            _baseURL1 = "Common";
            _httpService = httpService;
        }
       public async Task<OnBoarding> GetOnBoardingList(int TabIndex,Filter Filter)
        {
           return  await _httpService.PostAsync<OnBoarding,Filter>($"{_baseURL}/OnBoardingList?TabIndex={TabIndex}",Filter);
        }
        async Task<IList<OnBoardingListTabs>> IOnBoardingService.GetOnBoardingTabs()
        {
            return await _httpService.GetAsync<IList<OnBoardingListTabs>>($"{_baseURL}/GetOnBoardingListTabs");
        }
        async Task<OnBoarding> IOnBoardingService.GetJoiningChecklistDetail(Guid PreEnrollGI)
        {
            return await _httpService.GetAsync<OnBoarding>($"{_baseURL}/GetJoiningChecklistDetail?PreEnrollGI={PreEnrollGI}");
        }
        async Task<ResponseEntity> IOnBoardingService.JoiningCheckSave(OnBoarding model)
        {
            return await _httpService.PostAsync<ResponseEntity, OnBoarding>($"{_baseURL}/JoiningCheck", model);
        }

        public async Task<OfferLetter> GetofferLetterList(int DocType, Filter Filter)
        {
            return await _httpService.PostAsync<OfferLetter, Filter>($"{_baseURL}/GetOfferLetterListDetails?DocumentType={DocType}", Filter);
        }

        public async Task<Url> GetUrl()
        {
            return await _httpService.GetAsync<Url>($"{_baseURL1}/GetAPIUrl");
        }

        public async Task<Url> GetOfferLetterFileName(string Type)
        {
            return await _httpService.GetAsync<Url>($"{_baseURL1}/GetOfferLetter?Type={Type}");
        }
    }
}
