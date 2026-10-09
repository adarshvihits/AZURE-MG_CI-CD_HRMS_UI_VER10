using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IAppraisalTypeMasterService
    {
        public Task<AppraisalTypeMaster> GetAppraisalTypeList(string TabIndex);
        public Task<AppraisalTypeMaster> GetAppraisalTypeMasterDetails(Guid AppraisalTypeGI, Guid LogGI, string Type);
        public Task<ResponseEntity> Create(AppraisalTypeMaster model);
        public Task<ResponseEntity> ApproveOrReject(AppraisalTypeMaster model);
        public Task<ResponseEntity> Delete(Guid AppraisalTypeGI);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);

    }
}
