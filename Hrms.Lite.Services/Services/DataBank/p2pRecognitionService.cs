using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
   public class p2pRecognitionService: Ip2pRecognitionService
    {

        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly string _baseURL2;
        private readonly IServiceBase _httpService;
        public p2pRecognitionService(IServiceBase httpService)
        {
            _baseURL = "p2pRecognition";
            _baseURL1 = "Common";
            _baseURL2= "RewardsAndRecognition";
            _httpService = httpService;
        }

        public async Task<p2pRecognition> GetList(int radiomode)
        {
            return await _httpService.GetAsync<p2pRecognition>($"{_baseURL}/List?radiomode={radiomode}");
        }

        public async Task<p2pRecognition> Getp2pRecognitionDetails(Guid EmployeeGI, int RewardCode)
        {
            return await _httpService.GetAsync<p2pRecognition>($"{_baseURL}/Getp2pRecognitionDetails?EmployeeGI={EmployeeGI}&RewardCode={RewardCode}");

        }


        public Task<ResponseEntity> Create(p2pRecognition input)
        {
            return _httpService.PostAsync<ResponseEntity, p2pRecognition>($"{_baseURL}/Create", input);
        }
        public async Task<decimal> GetRewardPoints(int PointCriteriaCode)
        {
            return await _httpService.GetAsync<decimal>($"{_baseURL2}/GetRewardPoints?PointCriteriaCode={PointCriteriaCode}");

        }
    }
}
