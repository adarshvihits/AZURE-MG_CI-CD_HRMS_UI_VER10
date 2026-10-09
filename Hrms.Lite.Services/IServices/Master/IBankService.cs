using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IBankService
    {
        public Task<Bank> GetList(string TabIndex);
        public Task<ResponseEntity> Create(Bank input);
        public Task<Bank> GetBankDetails(Guid BankGI, Guid LogGI, string Type);
        public Task<ResponseEntity> ApproveOrReject(Bank model);
        public Task<ResponseEntity> Delete(Bank model);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);
    }
}
