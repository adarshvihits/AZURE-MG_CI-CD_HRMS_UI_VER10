using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IPlanningPeriodService
    {
        public Task<PlanningPeriod> GetPlanningPeriodList(string TabIndex);
        public Task<PlanningPeriod> GetPlanningPeriodDetails(Guid DisciplinaryGI, Guid LogGI, string type);
        public Task<ResponseEntity> Create(PlanningPeriod input);
        public Task<ResponseEntity> ApproveOrReject(PlanningPeriod model);
        public Task<ResponseEntity> PlanningPeriodDelete(Guid PlanningPeriodGI);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);

    }
}
