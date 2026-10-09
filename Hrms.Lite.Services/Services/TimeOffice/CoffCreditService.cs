using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TimeOffice;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.TimeOffice
{
    public class CoffCreditService: ICoffCreditService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public CoffCreditService(IServiceBase httpService)
        {
            _baseURL = "CoffCredit";
            _httpService = httpService;
        }


        public async Task<CoffCredit> GetList(DateTime? fromdate, DateTime? todate,string TabIndex,Filter filter)
        {
            return await _httpService.PostAsync<CoffCredit,Filter>($"{_baseURL}/ConfirmationList?FromDate={fromdate.Value.ToString("yyyy-MMM-dd")}&ToDate={todate.Value.ToString("yyyy-MMM-dd")}&TabIndex={TabIndex}", filter);
        }
        public async Task<ResponseEntity> CoffCreditApproveOrReject(CoffCredit model)
        {
            return await _httpService.PostAsync<ResponseEntity, CoffCredit>($"{_baseURL}/CoffCreditApproveOrReject", model);
        }
    }
}
