using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Services.IServices.TimeOffice;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.Master
{
    public class LeaveRuleService: ILeaveRuleService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public LeaveRuleService(IServiceBase httpService)
        {
            _baseURL = "LeaveRule";
            _httpService = httpService;
        }
        public async Task<LeaveRule> GetLeaveRuleDetails(int LeaveGroup, int Leave)
        {
            return await _httpService.GetAsync<LeaveRule>($"{_baseURL}/GetLeaveRule?LeaveGroup={LeaveGroup}&Leave={Leave}");
        }
        public Task<ResponseEntity> Save(LeaveRule input)
        {
            return _httpService.PostAsync<ResponseEntity, LeaveRule>($"{_baseURL}/LeaveRuleSave", input);
        }

    }
}
