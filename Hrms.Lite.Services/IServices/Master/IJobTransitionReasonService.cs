using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
 public   interface IJobTransitionReasonService
    {
        public Task<JobTransitionReason> GetJobTransitionReasonList(string TabIndex);
        public Task<ResponseEntity> Create(JobTransitionReason input);
        public Task<JobTransitionReason> GetJobTransitionReasonDetails(Guid CountryGI, Guid LogGI, string Type);
        public Task<ResponseEntity> ApproveOrReject(JobTransitionReason model);
        public Task<ResponseEntity> Delete(JobTransitionReason model);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);

    }
}
