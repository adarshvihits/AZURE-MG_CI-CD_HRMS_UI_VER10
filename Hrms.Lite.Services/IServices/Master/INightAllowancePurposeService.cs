using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface INightAllowancePurposeService
    {
        public Task<NightAllowancePurpose> GetList();
        public Task<NightAllowancePurpose> GetDetails(Guid purposeGi);
        Task<ResponseEntity> Save(NightAllowancePurpose model);
        Task<ResponseEntity> Delete(NightAllowancePurpose model);

    }
}
