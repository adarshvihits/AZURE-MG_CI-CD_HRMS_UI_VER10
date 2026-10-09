using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IPFGroupService
    {
        public Task<PFGroup> GetList(string TabIndex);
        public Task<ResponseEntity> Save(PFGroup Input);
        public Task<PFGroup> GetPFGroupDetails(Guid PFGroupGI, Guid LogGI, string Type);
        public Task<ResponseEntity> ApproveOrReject(PFGroup model);
        public Task<ResponseEntity> Delete(PFGroup Input);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);
    }
}
