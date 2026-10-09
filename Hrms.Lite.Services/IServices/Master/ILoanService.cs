using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface ILoanService
    {
        public Task<LoanSettings> GetList(string TabIndex);
        public Task<ResponseEntity> Create(LoanSettings input);
        public Task<LoanSettings> GetLoanDetails(Guid LoanGI, Guid LogGI, string Type);
        public Task<ResponseEntity> ApproveOrReject(LoanSettings model);
        public Task<ResponseEntity> Delete(LoanSettings model);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);
    }
}
