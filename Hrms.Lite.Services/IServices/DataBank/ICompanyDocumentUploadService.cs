using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface ICompanyDocumentUploadService
    {
        public Task<CompanyDocumentUpload> GetList(string Tabindex, int doctypeCode, Filter filter1);

        public Task<ResponseEntity> DocumentSave(CompanyDocumentUpload model);

        public Task<CompanyDocumentUpload> GetDocumentDetails(Guid EmployeeGI, int doctypeCode);

        public Task<ResponseEntity> CheckDocExists(CompanyDocumentUpload model);

        public Task<ResponseEntity> DeleteDocument(Popup model);

    }
}
