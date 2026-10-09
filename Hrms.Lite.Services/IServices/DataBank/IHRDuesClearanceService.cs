using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface IHRDuesClearanceService
    {
        public Task<EmployeeDueClearance> GetHrDuesClearanceList(Filter filter);
        public Task<EmployeeDueClearance> GetDuesClearance_EmployeeWiseDetails(Guid ResignationGI);
        public Task<ResponseEntity> Save(EmployeeDueClearance model);
    }
}
