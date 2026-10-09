using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.Master
{
    public class AllowanceDeductionVsErpAccountHeadService : IAllowanceDeductionVsErpAccountHeadService
    {
        private readonly string _baseURL;
        private readonly string _ClassificationVsERPcodeURL;
        private readonly IServiceBase _httpService;
        public AllowanceDeductionVsErpAccountHeadService(IServiceBase httpService)
        {
            _baseURL = "AllowanceDeductionVsErpAccountHead";
            _httpService = httpService;
        }
        public async Task<AccountsHead> GetList(string TabIndex, int GroupCode)
        {
            return await _httpService.GetAsync<AccountsHead>($"{_baseURL}/List?TabIndex={TabIndex}&GroupCode={GroupCode}");
        }
        public async Task<ResponseEntity> Create(AccountsHead model)
        {
            return await _httpService.PostAsync<ResponseEntity, AccountsHead>($"{_baseURL}/Create", model);
        }
        public async Task<DataSet> GetExcel(string TabIndex, int GroupCode)
        {
            return await _httpService.GetAsync<DataSet>($"{_baseURL}/GetAccountHead_Excel?TabIndex={TabIndex}&GroupCode={GroupCode}");
        }

 
    }
}
