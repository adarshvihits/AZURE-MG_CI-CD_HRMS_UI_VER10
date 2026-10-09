using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Hrms.Lite.Services.IServices.Master
{
  public  interface IEmployeeDocumentTypeService
    {
        public Task<EmployeeDocumentType> GetList(string TabIndex);
        public Task<EmployeeDocumentType> GetEmployeeDocumentTypeDetails(Guid DocumentGI, Guid LogGI, string type);
        public Task<ResponseEntity> Create(EmployeeDocumentType input);
        public Task<ResponseEntity> ApproveOrReject(EmployeeDocumentType model);
        public Task<ResponseEntity> Delete(EmployeeDocumentType model);
    }
}
