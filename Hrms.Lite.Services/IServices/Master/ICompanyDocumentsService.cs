using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface  ICompanyDocumentsService
    {
        public Task<CompanyDocuments> GetList(string TabIndex);
        public Task<CompanyDocuments> GetCompanyDocumentsDetails(Guid DocTypeGI, Guid LogGI, string type);
        public Task<ResponseEntity> Create(CompanyDocuments Input);


    }
}
