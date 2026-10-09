using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll.Reports
{
  public  interface IBankStatementReportService
    {

        //public Task<BankStatement> GetList(int MonthCode,int BankCode, Filter filter);
        public Task<BankStatement> GetList(BankStatement model);
        //public Task<DataSet> GetBankStatement_Excel(int SalaryPeriod, int BankCode, Filter filter);
        public Task<DataSet> GetBankStatement_Excel(BankStatement model);

        Task<ResponseEntity<BankStatement>> AccountNumberDuplication(BankStatement model);



    }
}
