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
    public class InsurancePolicyService : IInsurancePolicyService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public InsurancePolicyService(IServiceBase httpService)
        {
            _baseURL = "InsurancePolicy";
            _httpService = httpService;
        }
        public async Task<InsurancePolicy> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<InsurancePolicy>($"{_baseURL}/list?TabIndex={TabIndex}");
        }
        public async Task<InsuranceCompany> GetInsuranceCompanyList()
        {
            return await _httpService.GetAsync<InsuranceCompany>($"{_baseURL}/GetInsuranceCompanyList");
        }

        public Task<ResponseEntity> Create(InsurancePolicy input)
        {
            return _httpService.PostAsync<ResponseEntity, InsurancePolicy>($"{_baseURL}/Create", input);
        }
        public Task<ResponseEntity> InsuranceCompanySave(InsuranceCompany input)
        {
            return _httpService.PostAsync<ResponseEntity, InsuranceCompany>($"{_baseURL}/InsuranceCompanySave", input);
        }
        public async Task<InsurancePolicy> GetInsurancePolicyDetails(Guid LogGI, Guid InsurancePolicyGI, string Type)
        {
            return await _httpService.GetAsync<InsurancePolicy>($"{_baseURL}/GetInsurancePolicyDetails?LogGI={LogGI}&InsurancePolicyGI={InsurancePolicyGI}&Type={Type}");
        }
        public Task<ResponseEntity> ApproveOrReject(InsurancePolicy model)
        {
            return _httpService.PostAsync<ResponseEntity, InsurancePolicy>($"{_baseURL}/ApproveOrReject", model);
        }     
        public Task<ResponseEntity> Delete(InsurancePolicy model)
        {
            return _httpService.PostAsync<ResponseEntity, InsurancePolicy>($"{_baseURL}/Delete", model);
        }

    }
}
