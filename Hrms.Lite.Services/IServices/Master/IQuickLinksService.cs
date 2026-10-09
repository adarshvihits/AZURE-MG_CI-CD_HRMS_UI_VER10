using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IQuickLinksService
    {
      public  Task<QuickLinks> GetList(string TabIndex);
        public Task<ResponseEntity> Create(QuickLinks input);
        public Task<ResponseEntity> QuicklinkSave(QuickLinks model);
        public Task<ResponseEntity> QuicklinkUpdate(QuickLinks model);
        Task<QuickLinks> GetCombinationDetails(int TypeCode);
        public Task<QuickLinks> GetQuickLinksDetails(Guid QuickLinksGI);
        public Task<ResponseEntity> Delete(QuickLinks model);
        public Task<ResponseEntity> ApproveOrReject(QuickLinks model);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);
    }
}
