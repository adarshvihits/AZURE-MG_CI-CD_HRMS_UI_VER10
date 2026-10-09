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
    public class RewardReferingService: IRewardReferingService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public RewardReferingService(IServiceBase httpService)
        {
            _baseURL = "RewardRefering";
            _baseURL1 = "Common";
            _httpService = httpService;
        }
        public async Task<RewardRefering> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<RewardRefering>($"{_baseURL}/List?TabIndex={TabIndex}");
        }
        public Task<ResponseEntity> Create(RewardRefering input)
        {
            return _httpService.PostAsync<ResponseEntity, RewardRefering>($"{_baseURL}/Create", input);
        }
        public Task<RewardRefering> GetDetails(Guid RewardsReferTypeGI, Guid LogGI, string type)
        {
            return _httpService.GetAsync<RewardRefering>($"{_baseURL}/GetDetails?RewardsReferTypeGI={RewardsReferTypeGI}&LogGI={LogGI}&Type={type}");
        }
        public async Task<ResponseEntity> Delete(RewardRefering model)
        {
            return await _httpService.PostAsync<ResponseEntity, RewardRefering>($"{_baseURL}/Delete", model);

        }
        public Task<ResponseEntity> ApproveOrReject(RewardRefering model)
        {
            return _httpService.PostAsync<ResponseEntity, RewardRefering>($"{_baseURL}/ApproveOrReject", model);
        }
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }

    }
}
