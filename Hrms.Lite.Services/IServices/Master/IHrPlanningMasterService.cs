using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IHrPlanningMasterService
    {
        public Task<HRPlanningMaster> GetHRPlanningList(string TabIndex, int? Branch = null, int? PlanningPeriod = null);
        public Task<HRPlanningMaster> GetHRPlanningMasterDetails(int Logcode, Guid LogGI, string Type, int? Branch = null, int? PlanningPeriod = null);
        public Task<ResponseEntity> Create(HRPlanningMaster model, int Branch, int PlanningPeriod);
        public Task<ResponseEntity> ApproveOrReject(HRPlanningMaster model);
        public Task<ResponseEntity> Delete(Guid HRPlanningGI);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);
        public Task<ResponseEntity> CarryForwardPlan(int Branch, int FromPlanningPeriod, int ToPlanningPeriod);

    }
}
