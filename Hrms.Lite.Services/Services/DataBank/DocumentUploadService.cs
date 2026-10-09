using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
    public class DocumentUploadService : IDocumentUploadService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public DocumentUploadService(IServiceBase httpService)
        {
            _baseURL = "DocumentUpload";
            _httpService = httpService;
        }
        async Task<DocumentUpload> IDocumentUploadService.GetDocumentUploadList(int Tabindex, int SearchId,Filter filter)
        {
            return await _httpService.PostAsync<DocumentUpload,Filter>($"{_baseURL}/GetDocumentUploadList?Tabindex={Tabindex}&SearchId={SearchId}",filter);
        }
        async Task<DocumentUpload> IDocumentUploadService.GetIdentityDocumentDetailsList(Guid EmployeeGI)
        {
            return await _httpService.GetAsync<DocumentUpload>($"{_baseURL}/GetIdentityDocumentDetailsList?EmployeeGI={EmployeeGI}");
        }
        async Task<ResponseEntity> IDocumentUploadService.OtherDocumentSave(DocumentUpload model)
        {
            return await _httpService.PostAsync<ResponseEntity, DocumentUpload>($"{_baseURL}/OtherDocumentUploadSave", model);
        }
        async Task<ResponseEntity> IDocumentUploadService.IdentityDocumentSave(DocumentUpload model)
        {
            return await _httpService.PostAsync<ResponseEntity, DocumentUpload>($"{_baseURL}/IdentityDocumentUploadSave", model);
        }
        async Task<DocumentUpload> IDocumentUploadService.GetIdentityDocumentDetails(Guid EmployeeGI, string DocType)
        {
            return await _httpService.GetAsync<DocumentUpload>($"{_baseURL}/GetIdentityDocumentDetails?EmployeeGI={EmployeeGI}&DOC_TYPE={DocType}");
        }
        async Task<DocumentUpload> IDocumentUploadService.GetOtherDocumentList(Guid EmployeeGI)
        {
            return await _httpService.GetAsync<DocumentUpload>($"{_baseURL}/GetOtherDocumentList?EmployeeGI={EmployeeGI}");
        }
        async Task<DocumentUpload> IDocumentUploadService.GetOtherDocumentDetails(Guid OtherDocGI, Guid LogGI)
        {
            return await _httpService.GetAsync<DocumentUpload>($"{_baseURL}/GetOtherDocumentDetails?OtherDocGI={OtherDocGI}&LogGI={LogGI}");
        }
        async Task<ResponseEntity> IDocumentUploadService.IdentityDocumentDelete(Guid EmployeeGI, string DocType)
        {
            return await _httpService.DeleteAsync<ResponseEntity>($"{_baseURL}/IdentityDocumentUploadDelete?EmployeeGI={EmployeeGI}&DocType={DocType}");
        }
        async Task<ResponseEntity> IDocumentUploadService.OtherDocumentDelete(Guid LogGI, Guid OtherDocGI)
        {
            return await _httpService.DeleteAsync<ResponseEntity>($"{_baseURL}/OtherDocumentUploadDelete?LogGI={LogGI}&OtherDocGI={OtherDocGI}");
        }

        public async Task<DataSet> GetDocumentUploadList_Excel(int Tabindex, int SearchId, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetDocumentUploadList_Excel?Tabindex={Tabindex}&SearchId={SearchId}", filter);
        }
       public async Task<File> GetDocumentFileData(Guid EmployeeGI, string DocType, string IdentityDocType, Guid OtherDocGI)

        {
            return await _httpService.GetAsync<File>($"{_baseURL}/GetDocumentFileData?EmployeeGI={EmployeeGI}&DocType={DocType}&IdentityDocType={IdentityDocType}&OtherDocGI={OtherDocGI}");   
        }
        public async Task<List<PanDocument>> GetAllPanDocuments()
        {
            return await _httpService.GetAsync< List<PanDocument>>($"{_baseURL}/GetAllPanDocuments");
        }
    }
}
