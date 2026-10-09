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
    public class AllowanceDeductionVsAccountHeadService: IAllowanceDeductionVsAccountHeadService
    {
        private readonly string _baseURL;
        private readonly string _ClassificationVsERPcodeURL;
        private readonly IServiceBase _httpService;
        public AllowanceDeductionVsAccountHeadService(IServiceBase httpService)
        {
            _baseURL = "AllowanceDeductionVsAccountHead";
            _ClassificationVsERPcodeURL = "ClassificationVsERPcode";
            _httpService = httpService;
        }
        public async Task<AccountsHead> GetList(string TabIndex,int GroupCode)
        {
            return await _httpService.GetAsync<AccountsHead>($"{_baseURL}/List?TabIndex={TabIndex}&GroupCode={GroupCode}");
        }
        public async Task<ResponseEntity> Create(AccountsHead model)
        {
            return await _httpService.PostAsync<ResponseEntity, AccountsHead>($"{_baseURL}/Create", model);
        }
        public async Task<DataSet> GetExcel(string TabIndex,int GroupCode)
        {
            return await _httpService.GetAsync<DataSet>($"{_baseURL}/GetAccountHead_Excel?TabIndex={TabIndex}&GroupCode={GroupCode}");
        }

       public async Task<AccountsHead> ClassificationVsERPcodeList(int ClassificationCode)
        {
            return await _httpService.GetAsync<AccountsHead>($"{_ClassificationVsERPcodeURL}/List?ClassificationCode={ClassificationCode}");
        }

        public async Task<DataSet> ClassificationVsERPList_Excel(int ClassificationCode)
        {
            return await _httpService.GetAsync<DataSet>($"{_ClassificationVsERPcodeURL}/ClassificationVsERPList_Excel?ClassificationCode={ClassificationCode}");
        }

        public async Task<ResponseEntity> ClassificationVsERPcodeSave(AccountsHead input)
        {
            return await _httpService.PostAsync<ResponseEntity, AccountsHead>($"{_ClassificationVsERPcodeURL}/Create", input);
        }
    }   
}
