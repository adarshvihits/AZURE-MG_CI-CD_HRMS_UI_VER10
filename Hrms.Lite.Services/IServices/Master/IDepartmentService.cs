using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
   public interface IDepartmentService
    {
        public Task<Department> GetList(string TabIndex);
        public Task<ResponseEntity> Create(Department input);
        public Task<Department> GetDepartmentDetails(Guid DepartmentGI, Guid LogGI, string Type);
        public Task<ResponseEntity> Delete(Department model);
        public Task<ResponseEntity> ApproveOrReject(Department model);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name,int code );
        public Task<List<DepartmentVsSectionBT>> GetSectionBTGridFill();

    }
}
