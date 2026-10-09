using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
   public interface IEvaluationPeriodService
    {
        public Task<EvaluationPeriodMaster> GetEvaluationPeriodList(string TabIndex);
        public Task<EvaluationPeriodMaster> GetEvaluationPeriodMasterDetails(Guid DisciplinaryGI, Guid LogGI, string type);
        public Task<ResponseEntity> Create(EvaluationPeriodMaster input);
        public Task<ResponseEntity> ApproveOrReject(EvaluationPeriodMaster model);
        public Task<ResponseEntity> EvaluationPeriodDelete(Guid EvaluationPeriodGI);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);

    }
}
