using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface IWorkUnitTransferRequestApprovalService
    {
        public Task<WorkUnitTransferRequest> GetWorkUnitTransferRequestApprovalList();
        public Task<WorkUnitTransferRequest> GetEmployeeWorkUnitDetails(Guid EmployeeGI);
        public Task<ResponseEntity> ApproveOrReject(WorkUnitTransferRequest model);
    }
}
