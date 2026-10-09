using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IVDAGroupService
    {

        public Task<VDAGroup> GetList(string TabIndex);
        public Task<ResponseEntity> Create(VDAGroup input);
        public Task<VDAGroup> VDAGroupDetails(Guid VDAGroupGI, Guid LogGI, string Type);
        public Task<ResponseEntity> ApproveOrReject(VDAGroup model);
        public Task<ResponseEntity> Delete(VDAGroup model);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);

        // public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);
    }
}
