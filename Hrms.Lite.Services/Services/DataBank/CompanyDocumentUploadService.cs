using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
    public class CompanyDocumentUploadService : ICompanyDocumentUploadService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public CompanyDocumentUploadService(IServiceBase httpService)
        {
            _baseURL = "CompanyDocumentUpload";
            _httpService = httpService;
        }

        public async Task<CompanyDocumentUpload> GetList(string Tabindex, int doctypeCode, Filter filter)
        {
            return await _httpService.PostAsync<CompanyDocumentUpload, Filter>($"{_baseURL}/GetList?Tabindex={Tabindex}&doctypeCode={doctypeCode}", filter);
        }

        public async Task<ResponseEntity> DocumentSave(CompanyDocumentUpload model)
        {
            return await _httpService.PostAsync<ResponseEntity, CompanyDocumentUpload>($"{_baseURL}/DocumentSave", model);
        }

        public async Task<CompanyDocumentUpload> GetDocumentDetails(Guid EmployeeGI, int doctypeCode)
        {
            return await _httpService.GetAsync<CompanyDocumentUpload>($"{_baseURL}/GetDocumentDetails?EmployeeGI={EmployeeGI}&doctypeCode={doctypeCode}");
        }

        public async Task<ResponseEntity> CheckDocExists(CompanyDocumentUpload model)
        {
            return await _httpService.PostAsync<ResponseEntity,CompanyDocumentUpload>($"{_baseURL}/CheckDocExists",model);
        }

        public Task<ResponseEntity> DeleteDocument(Popup model)
        {
            return _httpService.PostAsync<ResponseEntity, Popup>($"{_baseURL}/Delete", model);
        }
    }
}
