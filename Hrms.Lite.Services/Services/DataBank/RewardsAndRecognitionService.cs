using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
   public class RewardsAndRecognitionService : IRewardsAndRecognitionService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public RewardsAndRecognitionService(IServiceBase httpService)
        {
            _baseURL = "RewardsAndRecognition";
            _httpService = httpService;
        }
       public async Task<IList<RewardandRecognitionTabs>> GetRewardsAndRecognitionTabs()
        {

            return await _httpService.GetAsync<IList<RewardandRecognitionTabs>>($"{_baseURL}/GetRewardsAndRecognitionTabs");
        }
        public async Task<RewardsAndRecognition> GetRewardsAndRecognitionList(int Tabindex, int SearchId, int? RewardCode,Filter filter)
        {
            return await _httpService.PostAsync<RewardsAndRecognition, Filter>($"{_baseURL}/GetRewardsAndRecognitionList?Tabindex={Tabindex}&SearchId={SearchId}&RewardCode={RewardCode}", filter);



        }
  
        public  async  Task<ResponseEntity> NewRequest(RewardsRequest model)
        {
            return await _httpService.PostAsync<ResponseEntity, RewardsRequest>($"{_baseURL}/RewardRequestSave",  model) ;
        }
        public async Task<RewardsRequest> GetRewardsLogDetails(Guid RewardsAndRecognitionGI)
        {
            return await _httpService.GetAsync<RewardsRequest>($"{_baseURL}/GetRewardsLogDetails?RewardsAndRecognitionGI={RewardsAndRecognitionGI}");
        }


    public async Task<ResponseEntity> Approve(RewardsRequest model)
        {
            return await _httpService.PostAsync<ResponseEntity, RewardsRequest>($"{_baseURL}/Approve",model);
        }

        public async Task<RewardsAndRecognition> GetRewardsAndRecognitionDetails(Guid RewardsAndRecognitionGI)
        {
            return await _httpService.GetAsync<RewardsAndRecognition>($"{_baseURL}/GetRewardsAndRecognitionDetails?RewardsAndRecognitionGI={RewardsAndRecognitionGI}");
        }

      public async Task<IList<RewardsLog>> GetRewardLogList(Guid EmployeeGI)
        {
            return await _httpService.GetAsync<IList<RewardsLog>>($"{_baseURL}/GetRewardLogList?EmployeeGI={EmployeeGI}");

        }
        public async Task<decimal > GetRewardPoints(int PointCriteriaCode)
        {
            return await _httpService.GetAsync<decimal>($"{_baseURL}/GetRewardPoints?PointCriteriaCode={PointCriteriaCode}");

        }

      public async Task<RewardsAndRecognition> GetEmployeeSummary(Guid EmployeeGI, int SearchId)
        {
            return await _httpService.GetAsync<RewardsAndRecognition>($"{_baseURL}/GetEmployeeSummary?EmployeeGI={EmployeeGI}&SearchId={SearchId}");
        }

    public    async Task<ResponseEntity> Delete(Guid RewardsAndRecognitionGI)
        {
            return await _httpService.DeleteAsync<ResponseEntity>($"{_baseURL}/Delete?RewardsAndRecognitionGI={RewardsAndRecognitionGI}");
        }

      
    }
}
