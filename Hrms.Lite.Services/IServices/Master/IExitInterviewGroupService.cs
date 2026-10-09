using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
  public  interface IExitInterviewGroupService
    {
        public Task<ExitInterviewGroup> GetList(string TabIndex);
        public Task<ResponseEntity> Create(ExitInterviewGroup input);
        public Task<ExitInterviewGroup> GetinterviewGroupDetails(Guid InterviewGruopGI, Guid LogGI, string Type);
        public Task<ResponseEntity> ApproveOrReject(ExitInterviewGroup model);
        public Task<ResponseEntity> Delete(ExitInterviewGroup model);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);

    }
}
