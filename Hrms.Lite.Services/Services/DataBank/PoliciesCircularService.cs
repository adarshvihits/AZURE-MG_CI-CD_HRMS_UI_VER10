using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
    public class PoliciesCircularService: IPoliciesCircularService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public PoliciesCircularService(IServiceBase httpService)
        {
            _baseURL = "PoliciesCirculars";
            _httpService = httpService;
        }
        public async Task<PoliciesCirculars> GetList()
        {
            return await _httpService.GetAsync<PoliciesCirculars>($"{_baseURL}/List");
        }
        public async Task<PoliciesCirculars> GetPolicyDetails(Guid PolicyGI)
        {
            return await _httpService.GetAsync<PoliciesCirculars>($"{_baseURL}/Details?PolicyGI={PolicyGI}");
        }
        public async Task<ResponseEntity> PolicySave(PoliciesCirculars model)
        {
            return await _httpService.PostAsync<ResponseEntity, PoliciesCirculars>($"{_baseURL}/Create", model);
        }
        public async Task<ResponseEntity> PolicyUpdate(PoliciesCirculars model)
        {
            return await _httpService.PostAsync<ResponseEntity, PoliciesCirculars>($"{_baseURL}/Update", model);
        }
        public async Task<PoliciesCirculars> GetCategoryDtls()
        {
            return await _httpService.GetAsync<PoliciesCirculars>($"{_baseURL}/GetCategoryDtls");
        }
        public async Task<PoliciesCirculars> GetCombinationDetails(int id)
        {
            return await _httpService.GetAsync<PoliciesCirculars>($"{_baseURL}/CombinationDetails?Typecode={id}");
        }
        public async Task<ResponseEntity> CategoryResultSave(List<PolicyCategory> input)
        {
            return await _httpService.PostAsync<ResponseEntity, List<PolicyCategory>>($"{_baseURL}/CatSave", input);
        }
        public async Task<ResponseEntity> Delete(PoliciesCirculars input)
        {
            return await _httpService.PostAsync<ResponseEntity, PoliciesCirculars>($"{_baseURL}/Delete", input);
        }
        public async Task<ResponseEntity> DeleteCategory(PolicyCategory input)
        {
            return await _httpService.PostAsync<ResponseEntity, PolicyCategory>($"{_baseURL}/CatDelete", input);
        }
        public async Task<File> DownloadDoc(Guid PolicyGI)
        {
            return await _httpService.GetAsync<File>($"{_baseURL}/DownloadDoc?PolicyGI={PolicyGI}");
        }
    }
}
