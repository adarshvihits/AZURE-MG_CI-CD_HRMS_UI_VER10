using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IDeductionService
    {
        public Task<Deduction> GetList(string TabIndex);
        public Task<ResponseEntity> Create(Deduction input);
        public Task<Deduction> GetDeductionDetails(Guid DeductionGI, Guid LogGI, string Type);
        public Task<ResponseEntity> Delete(Deduction model);
        public Task<ResponseEntity> ApproveOrReject(Deduction model);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);
    }
}
