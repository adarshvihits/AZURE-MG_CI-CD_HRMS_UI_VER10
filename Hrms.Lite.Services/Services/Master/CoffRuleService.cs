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
    public class CoffRuleService: ICoffRuleService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public CoffRuleService(IServiceBase httpService)
        {
            _baseURL = "CoffLeaveRule";
            _httpService = httpService;
        }
        public Task<CoffLeaveRule> GetCoffRuleDetails(int Leavegroup)
        {
            return _httpService.GetAsync<CoffLeaveRule>($"{_baseURL}/GetCoffLeaveRule?LeaveGroup={Leavegroup}");
        }
        public Task<ResponseEntity> Create(CoffLeaveRule model)
        {
            return _httpService.PostAsync<ResponseEntity, CoffLeaveRule>($"{_baseURL}/CoffLeaveRuleSave", model);
        }

    }
}
