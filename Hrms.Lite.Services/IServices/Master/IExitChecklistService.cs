using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
 public  interface IExitChecklistService
    {
        public Task<ExitCheckList> GetList(string TabIndex);
        public Task<ResponseEntity> Create(ExitCheckList Input);
        public Task<ExitCheckList> GetExitChecklistDetails(Guid ExitCheckListGI, Guid LogGI, string Type);
        public Task<ResponseEntity> Delete(ExitCheckList Input);
        public Task<ResponseEntity> ApproveOrReject(ExitCheckList model);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);
    }
}
