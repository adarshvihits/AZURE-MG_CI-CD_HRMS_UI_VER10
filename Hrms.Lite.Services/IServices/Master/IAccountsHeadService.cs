using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
  public  interface IAccountsHeadService
    {
        public Task<AccountsHead> GetList(string TabIndex);
        public Task<ResponseEntity> Create(AccountsHead input);
        public Task<AccountsHead> GetAccountsDetails(Guid AccountsGI, Guid LogGI, string Type);
        public Task<ResponseEntity> ApproveOrReject(AccountsHead model);
        public Task<ResponseEntity> Delete(AccountsHead model);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);
    }
}
