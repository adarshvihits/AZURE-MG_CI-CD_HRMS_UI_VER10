using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
   public interface IMedicalCheckupTypeService
    {
        public Task<CheckupType> GetList(string TabIndex);
        public Task<ResponseEntity> Create(CheckupType input);
        public Task<CheckupType> GetCheckUpDetails(Guid CheckupTypeGI, Guid LogGI, string Type);
        public Task<ResponseEntity> ApproveOrReject(CheckupType model);
        public Task<ResponseEntity> Delete(CheckupType input);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);

    }
}
