using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TimeOffice;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.TimeOffice
{
    public class PLCreditService : IPLCreditService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public PLCreditService(IServiceBase httpService)
        {
            _baseURL = "PLCredit";
            _httpService = httpService;
        }

        public async Task<PLCredit> GetPLCreditList(int LeavePeriodCode, int LeaveCode,Filter filter1)
        {
            return await _httpService.PostAsync<PLCredit, Filter>($"{_baseURL}/GetPLCreditList?LeavePeriod={LeavePeriodCode}&LeaveCode={LeaveCode}", filter1);
        }

        public async Task<DataSet> GetPLCredit_Excel(int LeavePeriodCode, int LeaveCode,Filter filter1)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetPLCredit_Excel?LeavePeriod={LeavePeriodCode}&LeaveCode={LeaveCode}", filter1);
        }

        public Task<ResponseEntity> PLCreditSave(PLCredit model)
        {
            return _httpService.PostAsync<ResponseEntity, PLCredit>($"{_baseURL}/PLCreditSave", model);
        }  
        
        public Task<ResponseEntity> PLCreditBatchUpdate(PLCredit model)
        {
            return _httpService.PostAsync<ResponseEntity, PLCredit>($"{_baseURL}/PLCreditBatchUpdate", model);
        }

    }
}
