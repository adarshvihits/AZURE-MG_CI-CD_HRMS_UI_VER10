using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IPTGroupService
    {
        public Task<PTGroup> GetList(string TabIndex);
        public Task<ResponseEntity> Create(PTGroup input);
        public Task<PTGroup> GetPTGroupDetails(Guid PtGroupGI, Guid LogGI, string Type);
        public Task<ResponseEntity> ApproveOrReject(PTGroup model);
        public Task<ResponseEntity> Delete(PTGroup input);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);
    }
}
