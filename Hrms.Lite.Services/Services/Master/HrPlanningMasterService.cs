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
    public class HrPlanningMasterService : IHrPlanningMasterService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;

        private readonly IServiceBase _httpService;
        public HrPlanningMasterService(IServiceBase httpService)
        {
            _baseURL = "HRPlanningMaster";
            _baseURL1 = "Common";
            _httpService = httpService;
        }
        async Task<HRPlanningMaster> IHrPlanningMasterService.GetHRPlanningList(string TabIndex, int? Branch, int? PlanningPeriod)
        {
            return await _httpService.GetAsync<HRPlanningMaster>($"{_baseURL}/GetHRPlanningList?TabIndex={TabIndex}&Branch={Branch}&PlanningPeriod={PlanningPeriod}");
        }
        async Task<HRPlanningMaster> IHrPlanningMasterService.GetHRPlanningMasterDetails(int Logcode, Guid LogGI, string Type, int? Branch = null, int? PlanningPeriod = null)
        {
            return await _httpService.GetAsync<HRPlanningMaster>($"{_baseURL}/GetHRPlanningMasterDetails?Logcode={Logcode}&LogGI={LogGI}&Type={Type}&Branch={Branch}&PlanningPeriod={PlanningPeriod}");
        }
        async Task<ResponseEntity> IHrPlanningMasterService.Create(HRPlanningMaster model, int Branch, int PlanningPeriod)
        {
            return await _httpService.PostAsync<ResponseEntity, HRPlanningMaster>($"{_baseURL}/Create?Branch={Branch}&PlanningPeriod={PlanningPeriod}", model);
        }
        async Task<ResponseEntity> IHrPlanningMasterService.ApproveOrReject(HRPlanningMaster model)
        {
            return await _httpService.PostAsync<ResponseEntity, HRPlanningMaster>($"{_baseURL}/ApproveOrReject", model);
        }
        async Task<ResponseEntity> IHrPlanningMasterService.Delete(Guid HRPlanningGI)
        {
            return await _httpService.DeleteAsync<ResponseEntity>($"{_baseURL}/Delete?HRPlanningGI={HRPlanningGI}");
        }

        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
        public async Task<ResponseEntity> CarryForwardPlan(int Branch, int FromPlanningPeriod, int ToPlanningPeriod)
        {
            return await _httpService.PostAsync<ResponseEntity, HRPlanningMaster>($"{_baseURL}/CarryForwardPlan?Branch={Branch}&FromPlanningPeriod={FromPlanningPeriod}&ToPlanningPeriod={ToPlanningPeriod}", new HRPlanningMaster());
        }
    }
}
