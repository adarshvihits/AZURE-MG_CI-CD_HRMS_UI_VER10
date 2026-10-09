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
    public interface IDealerUndertakingService
    {
        public Task<DealerUndertaking> DealerUndertakingList(String TabIndex);
        public Task<DealerUndertaking> GetDealerUndertaking(Guid EmployeeGI, String TabIndex);
        Task<ResponseEntity> SaveDealerUndertakingDetails(DealerUndertaking Model);
        public Task<DealerUndertaking> GetDealerUndertakingDetails(Guid EmployeeGI,int RequestCode, String TabIndex);
    }
}
