using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank.Reports
{
    public class TransactionLogService : ITransactionLogService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public TransactionLogService(IServiceBase httpService)
        {
            _baseURL = "TransactionLog";
            _httpService = httpService;
        }
        public async Task<TransactionLog> GetList(int Tab, string Fromdate, string Todate, int UserCode, int MenuCode, Filter filter)
        {
            return await _httpService.PostAsync<TransactionLog,Filter>($"{_baseURL}/GetTransactionLog?Tab={Tab}&Fromdate={Fromdate}&Todate={Todate}&UserCode={UserCode}&MenuCode={MenuCode}", filter);
        }
        public async Task<DataSet> TransactionLog_Excel(int Tab, string Fromdate, string Todate, int UserCode, int MenuCode, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/TransactionLog_Excel?Tab={Tab}&Fromdate={Fromdate}&Todate={Todate}&UserCode={UserCode}&MenuCode={MenuCode}", filter);
        }
    }
}
