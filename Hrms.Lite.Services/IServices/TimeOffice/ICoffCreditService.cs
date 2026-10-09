using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TimeOffice
{
    public interface ICoffCreditService
    {
        public Task<CoffCredit> GetList(DateTime? fromdate, DateTime? todate, string TabIndex, Filter filter);
        Task<ResponseEntity> CoffCreditApproveOrReject(CoffCredit model);
    }
}
