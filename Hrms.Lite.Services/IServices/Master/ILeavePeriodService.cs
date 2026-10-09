using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface ILeavePeriodService
    {
        public Task<LeavePeriod> GetList(string TabIndex);
        public Task<ResponseEntity> Create(LeavePeriod input);
        public Task<LeavePeriod> GetLeavePeriodDetails(Guid LeavePeriodGI, Guid LogGI, string Type);
        public Task<ResponseEntity> ApproveOrReject(LeavePeriod model);
        public Task<ResponseEntity> Delete(LeavePeriod model);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);
    }
}
