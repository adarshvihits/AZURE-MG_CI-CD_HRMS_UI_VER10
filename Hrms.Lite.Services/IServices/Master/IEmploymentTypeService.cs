using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
   public interface IEmploymentTypeService
    {
        public Task<EmploymentType> GetList(string TabIndex);
        public Task<ResponseEntity> Create(EmploymentType input);
        public Task<EmploymentType> GetEmploymentTypeDetails(Guid EmploymentGI, Guid LogGI, string Type);
        public Task<ResponseEntity> ApproveOrReject(EmploymentType model);
        public Task<ResponseEntity> Delete(EmploymentType model);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);

    }
}
