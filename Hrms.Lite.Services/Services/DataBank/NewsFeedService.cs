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
   public class NewsFeedService :INewsFeedService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public NewsFeedService(IServiceBase httpService)
        {
            _baseURL = "NewsFeed";
            _httpService = httpService;
        }
        public async Task<NewsFeed> GetList()
        {
            return await _httpService.GetAsync<NewsFeed>($"{_baseURL}/List");
        }
        public async Task<NewsFeed> GetNewsFeedCombinationDetails(int id)
        {
            return await _httpService.GetAsync<NewsFeed>($"{_baseURL}/CombinationDetails?Typecode={id}");
        }
        public async Task<ResponseEntity> NewsFeedSave(NewsFeed model)
        {
            return await _httpService.PostAsync<ResponseEntity, NewsFeed>($"{_baseURL}/Create", model);
        }
        public async Task<ResponseEntity> NewsFeedUpdate(NewsFeed model)
        {
            return await _httpService.PostAsync<ResponseEntity, NewsFeed>($"{_baseURL}/Update", model);
        }

        public async Task<NewsFeed> GetNewsFeedDetails(Guid NewsFeedGI)
        {
            return await _httpService.GetAsync<NewsFeed>($"{_baseURL}/Details?NewsFeedGI={NewsFeedGI}");
        }

        public async Task<ResponseEntity> Delete(NewsFeed model)
        {
            return await _httpService.PostAsync<ResponseEntity, NewsFeed>($"{_baseURL}/Delete", model);
        }

    }
}
