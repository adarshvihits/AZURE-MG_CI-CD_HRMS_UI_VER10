using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank.Reports
{
    public interface ITransactionLogService
    {
        public Task<TransactionLog> GetList(int Tab, string Fromdate, string Todate, int UserCode, int MenuCode,Filter filter);
        public Task<DataSet> TransactionLog_Excel(int Tab, string Fromdate, string Todate, int UserCode, int MenuCode, Filter filter);


    }
}
