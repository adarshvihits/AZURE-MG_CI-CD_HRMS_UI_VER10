using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.PayRoll
{
    public class MonthlyPaymentRemittanceService: IMonthlyPaymentRemittanceService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public MonthlyPaymentRemittanceService(IServiceBase httpService)
        {
            _baseURL = "MonthlyPaymentRemittance";
            _httpService = httpService;
        }


        public async Task<MonthlyPaymentRemittance> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<MonthlyPaymentRemittance>($"{_baseURL}/GetList?TabIndex={TabIndex}");
        }

        public Task<ResponseEntity> Create(string TabIndex, MonthlyPaymentRemittance input)
        {
            return _httpService.PostAsync<ResponseEntity, MonthlyPaymentRemittance>($"{_baseURL}/Create?TabIndex={TabIndex}", input);
        }


        public async Task<DataSet> MonthlyPay_Excel(string TabIndex, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/MonthlyPay_Excel?TabIndex={TabIndex}",filter);
        }


    }
}
