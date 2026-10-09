using Hrms.Lite.Shared.Admin;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Admin
{
    public interface IDocumentCategoryService
    {
        public Task<DocumentCategory> GetList(string TabIndex);
        public Task<ResponseEntity> Create(DocumentCategory input);
        public Task<DocumentCategory> GetDocCategoryDetails(Guid DocCategoryGI, string type);
        public Task<ResponseEntity> Delete(DocumentCategory model);
        public Task<DataSet> GetList_Excel(string TabIndex);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);
        public Task<DocumentCategory> GetParameterList();
    }
}
