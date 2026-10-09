using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared;
using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
   public class CertificatePolicyService : ICertificatePolicyService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public CertificatePolicyService(IServiceBase httpService)
        {
            _baseURL = "CertificatePolicy";
            _baseURL1 = "Common";
            _httpService = httpService;
        }

        public async Task<CertificatePolicy> GetList()
        {
            return await _httpService.GetAsync<CertificatePolicy>($"{_baseURL}/list");
        }

        public Task<ResponseEntity> Create(CertificatePolicy Model)
        {
            return _httpService.PostAsync<ResponseEntity, CertificatePolicy>($"{_baseURL}/Create", Model);
        }

        public Task<CertificatePolicy> GetDetails(int CertificateCode, string Type)
        {
            return _httpService.GetAsync<CertificatePolicy>($"{_baseURL}/GetDetails?CertificateCode={CertificateCode}&Type={Type}");
        }

        public Task<ResponseEntity> Delete( int CertificateCode)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL}/Delete?CertificateCode={CertificateCode}");
        }


        public Task<ResponseEntity> CreateCertificate(CertificatePolicy Model)
        {
            return _httpService.PostAsync<ResponseEntity, CertificatePolicy>($"{_baseURL}/CreateCertificate", Model);
        }


        public async Task<File> DownloadDoc()
        {
            return await _httpService.GetAsync<File>($"{_baseURL}/DownloadDocCetfication");
        }
        public Task<CertificatePolicy> GetPopUpDetails()
        {
            return _httpService.GetAsync<CertificatePolicy>($"{_baseURL}/GetPopUpDetails");
        }

    }
}
