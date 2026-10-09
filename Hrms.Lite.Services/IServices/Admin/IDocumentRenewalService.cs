using Hrms.Lite.Shared.Admin;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Admin
{
    public interface IDocumentRenewalService
    {
        Task<DocumentRenewal> GetDocumentRenewalList();
        Task<DocumentRenewal> GetDocumentRenewalDetails(Guid DocumentRenewalGI);
        Task<ResponseEntity> Create(DocumentRenewal Input);
        Task<ResponseEntity> Delete(DocumentRenewal model);
    }
}