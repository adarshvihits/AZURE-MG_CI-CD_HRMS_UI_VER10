using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll.Reports;
using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.PayRoll.Reports
{
    public class CashStatementService : ICashStatementService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public CashStatementService(IServiceBase httpService)
        {
            _baseURL = "CashStatement";
            _httpService = httpService;
        }
        public async Task<CashStatement> GetCashStatement(CashStatement model)
        {
            return await _httpService.PostAsync<CashStatement, CashStatement>($"{_baseURL}/GetCashStatement", model);
        }
        public async Task<DataSet> GetCashStatement_Excel(CashStatement model)
        {
            return await _httpService.PostAsync<DataSet, CashStatement>($"{_baseURL}/GetCashStatement_Excel", model);

        }
    }
}
