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
    public class ProductCompanyService : IProductCompanyService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public ProductCompanyService(IServiceBase httpService)
        {
            _baseURL = "ProductCompany";
            _baseURL1 = "Common";
            _httpService = httpService;
        }
        public async Task<ProductCompany> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<ProductCompany>($"{_baseURL}/List?TabIndex={TabIndex}");
        }
        public Task<ResponseEntity> Create(ProductCompany input)
        {
            return _httpService.PostAsync<ResponseEntity, ProductCompany>($"{_baseURL}/Create", input);
        }
        public Task<ProductCompany> GetProductCompanyDetails(Guid ProductGI, Guid LogGI, string Type)
        {
            return _httpService.GetAsync<ProductCompany>($"{_baseURL}/GetProductCompanyDetails?DesignationGI={ProductGI}&LogGI={LogGI}&Type={Type}");
        }
        public Task<ResponseEntity> Delete(ProductCompany input)
        {
            return _httpService.PostAsync<ResponseEntity, ProductCompany>($"{_baseURL}/Delete", input);
        }

        public Task<ResponseEntity> ApproveOrReject(ProductRequest model)
        {
            return _httpService.PostAsync<ResponseEntity, ProductRequest>($"{_baseURL}/ApproveOrReject", model);
        }

        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
