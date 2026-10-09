using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll.Reports
{
    public interface ICashStatementService
    {
        public Task<DataSet> GetCashStatement_Excel(CashStatement model);
        public Task<CashStatement> GetCashStatement(CashStatement model);
    }
}
