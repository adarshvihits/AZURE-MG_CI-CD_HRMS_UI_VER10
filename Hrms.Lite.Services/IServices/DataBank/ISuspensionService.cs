using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface ISuspensionService
    {
        public Task<Suspension> GetList(int TabIndex,Filter Filter);
        public Task<Suspension> GetDetails(Guid IncidentGI);
        public Task<ResponseEntity> Save(Suspension model);
        public Task<ResponseEntity> Delete(Guid IncidentGI);
        public Task<Suspension> GetTransactionHistoryList(string FromDate,string ToDate,Filter filter);
    }
}
