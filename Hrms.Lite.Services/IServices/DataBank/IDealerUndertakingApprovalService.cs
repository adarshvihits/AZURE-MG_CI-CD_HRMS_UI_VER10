using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface IDealerUndertakingApprovalService
    {
        public Task<DealerUndertakingApproval> DealerUndertakingApprovalList();
        public Task<DealerUndertakingApproval> GetDealerUndertakingApproval(Guid EmployeeGI);
        public Task<DealerUndertakingApproval> GetDealerUndertakingApprovalDetails(Guid EmployeeGI, int RequestCode);
        Task<ResponseEntity> ApproveDealerUndertakingDetails(DealerUndertakingApproval Model);

    }
}
