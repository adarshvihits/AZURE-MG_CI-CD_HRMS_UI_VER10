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
   public class DocumentRenewalService: IDocumentRenewalService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public DocumentRenewalService(IServiceBase httpService)
        {
            _baseURL = "DocumentRenewal";
            _httpService = httpService;
        }

        public async Task<DocumentRenewal> GetDocumentRenewalList()
        {
            return await _httpService.GetAsync<DocumentRenewal>($"{_baseURL}/DocumentRenewalList");
        }
        public async Task<DocumentRenewal> GetDocumentRenewalDetails(Guid DocumentRenewalGI)
        {
            return await _httpService.GetAsync<DocumentRenewal>($"{_baseURL}/GetDocumentRenewalDetails?DocumentRenewalGI={DocumentRenewalGI}");
        }
        public async Task<ResponseEntity> Create(DocumentRenewal Input)
        {
            return await _httpService.PostAsync<ResponseEntity, DocumentRenewal>($"{_baseURL}/Create", Input);
        }

        public async Task<ResponseEntity> Delete(DocumentRenewal Input)
        {
            return await _httpService.PostAsync<ResponseEntity, DocumentRenewal>($"{_baseURL}/Delete", Input);
        }
        //public async Task<ResponseEntity> Delete(DocumentRenewal model)
        //{
        //    return await _httpService.PostAsync<ResponseEntity, DocumentRenewal>($"{_baseURL}/Delete", model);
        //}
      

    }
}
