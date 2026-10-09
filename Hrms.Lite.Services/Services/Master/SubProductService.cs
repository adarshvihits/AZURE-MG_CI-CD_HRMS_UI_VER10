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
   public  class SubProductService : ISubProductService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public SubProductService(IServiceBase httpService)
        {
            _baseURL = "SubProduct";
            _baseURL1 = "Common";
            _httpService = httpService;

        }
        public async Task<SubProduct> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<SubProduct>($"{_baseURL}/List?TabIndex={TabIndex}");
        }

        public Task<ResponseEntity> Create(SubProduct input)
        {
            return _httpService.PostAsync<ResponseEntity, SubProduct>($"{_baseURL}/Create", input);
        }

        public async Task<SubProduct> GetSubProductDetails(Guid SubProductGI, Guid LogGI, string Type)
        {
            return await _httpService.GetAsync<SubProduct>($"{_baseURL}/SubProductDetails?SubProductGI={SubProductGI}&&LogGI={LogGI}&&Type={Type}");
        }

        public Task<ResponseEntity> Delete(SubProduct model)
        {
            return _httpService.PostAsync<ResponseEntity, SubProduct>($"{_baseURL}/Delete", model);
        }

        public Task<ResponseEntity> ApproveOrReject(SubProduct model)
        {
            return _httpService.PostAsync<ResponseEntity, SubProduct>($"{_baseURL}/ApproveOrReject", model);
        }
        public async Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }

        public async Task<DataSet> GetList_Excel(string TabIndex)
        {
            return await _httpService.GetAsync<DataSet>($"{_baseURL}/GetList_Excel?TabIndex={TabIndex}");
        }

    }
}
