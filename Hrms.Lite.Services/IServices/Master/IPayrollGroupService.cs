using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
   public interface IPayrollGroupService
    {
        public Task<PayrollGroup> GetList(string TabIndex);
        public Task<PayrollGroup> GetPayrollGroupDetails(Guid AllowanceGI, Guid LogGI, string Type);
        public Task<ResponseEntity> Create(PayrollGroup input);
        public Task<ResponseEntity> ApproveOrReject(PayrollGroup model);
        public Task<ResponseEntity> Delete(PayrollGroup model);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);
    }
}
