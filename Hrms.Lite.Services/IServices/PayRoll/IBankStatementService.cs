using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll
{
  public  interface IBankStatementService
    {
        public Task<System.Data.DataSet> GetBankStatementDetails(BankStatement model);

        Task<ResponseEntity> SaveSFTPFileName(int? MonthCode,string strfName);
        public Task<ResponseEntity> CheckIfAlreadyPostedtoSFTP(int MonthCode);
      
    }

}
