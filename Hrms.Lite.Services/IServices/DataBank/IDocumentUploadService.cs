using Hrms.Lite.Shared;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface IDocumentUploadService
    {
        public Task<DocumentUpload> GetDocumentUploadList(int Tabindex, int SearchId,Filter filter);
        public Task<DocumentUpload> GetOtherDocumentList(Guid EmployeeGI);
        public Task<DocumentUpload> GetIdentityDocumentDetailsList(Guid EmployeeGI);
        public Task<ResponseEntity> OtherDocumentSave(DocumentUpload model);
        public Task<ResponseEntity> IdentityDocumentSave(DocumentUpload model);
        public Task<DocumentUpload> GetIdentityDocumentDetails(Guid EmployeeGI,string DocType);
        public Task<DocumentUpload> GetOtherDocumentDetails(Guid OtherDocGI, Guid LogGI);
        public Task<ResponseEntity> IdentityDocumentDelete(Guid EmployeeGI,string DocType);
        public Task<ResponseEntity> OtherDocumentDelete(Guid LogGI,Guid OtherDocGI);
        Task<DataSet> GetDocumentUploadList_Excel(int Tabindex, int SearchId, Filter filter);
         Task<File> GetDocumentFileData(Guid EmployeeGI, string DocType, string IdentityDocType, Guid OtherDocGI);
        public Task<List<PanDocument>> GetAllPanDocuments();
    
    }
}
