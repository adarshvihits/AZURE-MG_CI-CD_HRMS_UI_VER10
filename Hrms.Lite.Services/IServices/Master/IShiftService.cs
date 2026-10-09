using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice.Generic.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
   public interface IShiftService
    {
        public Task<ShiftDetails> GetList(string TabIndex);
        public Task<ResponseEntity> Create(ShiftDetails input);
        public Task<ShiftDetails> GetShiftDetails(Guid ShiftGI, Guid LogGI, string Type);
        public Task<ResponseEntity> ApproveOrReject(ShiftDetails model);
        public Task<ResponseEntity> Delete(ShiftDetails model);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);
    }
}
