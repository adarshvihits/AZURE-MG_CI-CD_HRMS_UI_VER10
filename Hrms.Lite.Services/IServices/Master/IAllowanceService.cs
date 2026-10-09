using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IAllowanceService
    {
        public Task<Allowance> GetList(string TabIndex);
        public Task<ResponseEntity> Create(Allowance input);
        public Task<Allowance> GetAllowanceDetails(Guid AllowanceGI, Guid LogGI, string Type);
        public Task<Allowance> GetDeductionSettings();
     
        public Task<ResponseEntity> ApproveOrReject(Allowance model);
        public Task<ResponseEntity> Delete(Allowance model);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);

    }
}
