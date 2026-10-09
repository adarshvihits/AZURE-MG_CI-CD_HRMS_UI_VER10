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
    public class RewardsService : IRewardsService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public RewardsService(IServiceBase httpService)
        {
            _baseURL = "Rewards";
            _baseURL1 = "Common";
            _httpService = httpService;
        }
        public async Task<Rewards> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<Rewards>($"{_baseURL}/GetList?TabIndex={TabIndex}");
        }
        public Task<Rewards> GetRewardsDetails(Guid RewardGI, Guid LogGI, string type)
        {
            return _httpService.GetAsync<Rewards>($"{_baseURL}/GetRewardsDetails?RewardGI={RewardGI}&LogGI={LogGI}&Type={type}");
        }
        public async Task<List<RewardVsDepartment>> GetDepartmentGridFill()
        {
            return await _httpService.GetAsync<List<RewardVsDepartment>>($"{_baseURL}/GetDepartmentGridFill");
        }
        public Task<ResponseEntity> Create(Rewards input)
        {
            return _httpService.PostAsync<ResponseEntity, Rewards>($"{_baseURL}/RewardsSave", input);
        }
        public Task<ResponseEntity> ApproveOrReject(Rewards model)
        {
            return _httpService.PostAsync<ResponseEntity, Rewards>($"{_baseURL}/ApproveOrReject", model);
        }
        public async Task<ResponseEntity> Delete(Rewards model)
        {
            return await _httpService.PostAsync<ResponseEntity, Rewards>($"{_baseURL}/Delete", model);

        }
        public async Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
