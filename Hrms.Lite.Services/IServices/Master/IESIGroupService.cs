using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IESIGroupService
    {
        public Task<ESIGroup> GetList(string TabIndex);
        public Task<ResponseEntity> Create(ESIGroup input);
        public Task<ESIGroup> GetESIGroupDetails(Guid ESIGroupGI, Guid LogGI, string Type);
        public Task<ResponseEntity> ApproveOrReject(ESIGroup model);
        public Task<ResponseEntity> Delete(ESIGroup input);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);
    }
}
