using Hrms.Lite.Shared.Admin;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Admin
{
    public interface IDocumentsFolderService
    {
        public Task<DocumentsFolder> GetList(int DocCategoryCode);
        public Task<DocumentsFolder> GetDocumentDetails(int DocCategoryCode, Guid DocGI);
        public Task<ResponseEntity> DocumentSave(DocumentsFolder model);
        public Task<ResponseEntity> Delete(int DocCategoryCode, Guid DocGI);
    }
}
