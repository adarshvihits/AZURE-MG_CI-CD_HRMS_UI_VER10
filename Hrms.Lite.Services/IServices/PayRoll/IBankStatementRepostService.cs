using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll
{
   public interface IBankStatementRepostService
    {
        public Task<BankStatementRepost> GetList(int Month,int TabIndex);
        public Task<ResponseEntity> Save(BankStatementRepost input);
        public Task<ResponseEntity> RepostingApproval(BankStatementRepost input);
        
    }
}
