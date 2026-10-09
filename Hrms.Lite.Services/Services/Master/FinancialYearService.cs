using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.Master
{
   public class FinancialYearService:IFinancialYearService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public FinancialYearService(IServiceBase httpService)
        {
            _baseURL = "FinancialYear";
            _baseURL1 = "Common";
            _httpService = httpService;
        }
        public async Task<FinancialYear> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<FinancialYear>($"{_baseURL}/List?TabIndex={TabIndex}");
        }

        
        public async Task<FinancialYear> FinancialYeardetailsList(string fromDate, string ToDate)
        {
            return await _httpService.GetAsync<FinancialYear>($"{_baseURL}/GetMonthDetails?FromDate={fromDate}&ToDate={ToDate}");
        }
        public async Task<ResponseEntity> Save(FinancialYear model)
        {
            return await _httpService.PostAsync<ResponseEntity, FinancialYear>($"{_baseURL}/Create", model);
        }
      public  async Task<ResponseEntity> GetFinancialYRProceedValidation(FinancialYear input)
        {
            return await _httpService.PostAsync<ResponseEntity, FinancialYear>($"{_baseURL}/GetFinancialYRProceedValidation", input);
        }
        public Task<FinancialYear> GetFinancialYearDetails(Guid FinancialYearGI, Guid LogGI, string type)
        {
            return _httpService.GetAsync<FinancialYear>($"{_baseURL}/GetDetails?FinancialYearGI={FinancialYearGI}&LogGI={LogGI}&Type={type}");
        }
        public Task<ResponseEntity> ApproveOrReject(FinancialYear model)
        {
            return _httpService.PostAsync<ResponseEntity, FinancialYear>($"{_baseURL}/ApproveOrReject", model);
        }
        public Task<ResponseEntity> Delete(FinancialYear Input)
        {
            return _httpService.PostAsync<ResponseEntity, FinancialYear>($"{_baseURL}/Delete", Input);
        }

        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
