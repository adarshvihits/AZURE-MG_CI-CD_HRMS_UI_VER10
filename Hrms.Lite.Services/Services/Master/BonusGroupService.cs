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
    public class BonusGroupService : IBonusGroupService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public BonusGroupService(IServiceBase httpService)
        {
            _baseURL = "BonusGroup";           
            _httpService = httpService;
        }
        public async Task<BonusGroup> GetBonusGroupList()
        {
            return await _httpService.GetAsync<BonusGroup>($"{_baseURL}/GetBonusGroupList");
        }
        public async Task<BonusGroup> GetBonusGroupApplicableAllowance()
        {
            return await _httpService.GetAsync<BonusGroup>($"{_baseURL}/GetBonusGroupApplicableAllowance");
        }
        public async Task<BonusGroup> GetBonusGroupDetails(Guid BonusGroupGI)
        {
            return await _httpService.GetAsync<BonusGroup>($"{_baseURL}/GetBonusGroupDetails?BonusGroupGI={BonusGroupGI}");
        }
        public Task<ResponseEntity> Save(BonusGroup input, string mode)
        {
            return _httpService.PostAsync<ResponseEntity, BonusGroup>($"{_baseURL}/Create?mode={mode}", input);
        }

        public Task<ResponseEntity> Delete(BonusGroup model)
        {
            return _httpService.PostAsync<ResponseEntity, BonusGroup>($"{_baseURL}/Delete", model);
        }

    }
}
