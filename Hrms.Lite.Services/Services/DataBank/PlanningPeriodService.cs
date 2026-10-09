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
    public class PlanningPeriodService : IPlanningPeriodService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public PlanningPeriodService(IServiceBase httpService)
        {
            _baseURL1 = "Common";
            _baseURL = "PlanningPeriod";
            _httpService = httpService;
        }
        public async Task<PlanningPeriod> GetPlanningPeriodList(string TabIndex)
        {
            return await _httpService.GetAsync<PlanningPeriod>($"{_baseURL}/GetPlanningPeriodList?TabIndex={TabIndex}");
        }
        public Task<PlanningPeriod> GetPlanningPeriodDetails(Guid PlanningPeriodGI, Guid LogGI, string type)
        {
            return _httpService.GetAsync<PlanningPeriod>($"{_baseURL}/GetPlanningPeriodDetails?PlanningPeriodGI={PlanningPeriodGI}&LogGI={LogGI}&Type={type}");
        }
        public Task<ResponseEntity> Create(PlanningPeriod input)
        {
            return _httpService.PostAsync<ResponseEntity, PlanningPeriod>($"{_baseURL}/Create", input);
        }
        public Task<ResponseEntity> ApproveOrReject(PlanningPeriod model)
        {
            return _httpService.PostAsync<ResponseEntity, PlanningPeriod>($"{_baseURL}/ApproveOrReject", model);
        }
        public async Task<ResponseEntity> PlanningPeriodDelete(Guid PlanningPeriodGI)
        {
            return await _httpService.DeleteAsync<ResponseEntity>($"{_baseURL}/Delete?PlanningPeriodGI={PlanningPeriodGI}");
        }

        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
