using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TimeOffice
{
    public interface IPunchingRegularizationApprovalService
    {
        public Task<PunchingRegularizationApproval> GetList(Guid EmployeeGI);
        public Task<ResponseEntity> Approve(PunchingRegularizationApproval model);
        public Task<ResponseEntity> Reject(PunchingRegularizationApproval model);

    }
}
