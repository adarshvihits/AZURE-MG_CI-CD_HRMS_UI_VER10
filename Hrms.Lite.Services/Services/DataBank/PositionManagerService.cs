using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
    public class PositionManagerService : IPositionManagerService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public PositionManagerService(IServiceBase httpService)
        {
            _baseURL = "PositionManager";
            _httpService = httpService;
        }
        public async Task<List<PositionManager>> GetPositionManagerList(Filter filter)
        {
            return await _httpService.PostAsync<List<PositionManager>, Filter>($"{_baseURL}/GetPositionManagerList", filter);
        }
        public async Task<PositionManager> GetPositionManagerSummary(Filter filter)
        {
            return await _httpService.PostAsync<PositionManager, Filter>($"{_baseURL}/GetPositionManagerSummary", filter);
        }
        public async Task<PositionManager> GetPositionManagerDetails(Guid PositionGI)
        {
            return await _httpService.GetAsync<PositionManager>($"{_baseURL}/GetPositionManagerDetails?PositionGI={PositionGI}");
        }
    }
}
