using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IKeyPositionService
    {
        public Task<KeyPosition> GetList(string TabIndex);
        public Task<KeyPosition> KeyPositiondetails();
        public Task<ResponseEntity> Save(KeyPosition model);
        public Task<KeyPosition> GetKeyPositionDetails(Guid KeyPositionGI, Guid LogGI, string Type);
        public Task<ResponseEntity> ApproveOrReject(KeyPosition input);
        public Task<ResponseEntity> Delete(KeyPosition input);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);
    }
}
