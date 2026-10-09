using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Admin;
using Hrms.Lite.Shared.Admin;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.Admin
{
    public class RenewalsCategoryService : IRenewalsCategoryService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public RenewalsCategoryService(IServiceBase httpService)
        {
            _baseURL = "RenewalsCategory";
            _httpService = httpService;
        }

        public async Task<RenewalsCategory> GetRenewalsCategoryList(string TabIndex)
        {
            return await _httpService.GetAsync<RenewalsCategory>($"{_baseURL}/GetRenewalsCategoryList?TabIndex={TabIndex}");
        }
        public Task<ResponseEntity> Create(RenewalsCategory input)
        {
            return _httpService.PostAsync<ResponseEntity, RenewalsCategory>($"{_baseURL}/Create", input);
        }
        public Task<RenewalsCategory> GetRenewalsCategoryDetails(int RenewalsCategoryCode, string type)
        {
            return _httpService.GetAsync<RenewalsCategory>($"{_baseURL}/GetRenewalsCategoryDetails?RenewalsCategoryCode={RenewalsCategoryCode}&Type={type}");
        }
        public async Task<ResponseEntity> Delete(RenewalsCategory model)
        {
            return await _httpService.PostAsync<ResponseEntity, RenewalsCategory>($"{_baseURL}/Delete", model);

        }
        public async Task<DataSet> GetRenewalsCategoryList_Excel(string TabIndex)
        {
            return await _httpService.GetAsync<DataSet>($"{_baseURL}/GetRenewalsCategoryList_Excel?TabIndex={TabIndex}");
        }
    }
}
