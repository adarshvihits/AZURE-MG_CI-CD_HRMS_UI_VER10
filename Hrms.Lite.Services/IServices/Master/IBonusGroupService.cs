using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IBonusGroupService
    {
        Task<BonusGroup> GetBonusGroupList();
        Task<BonusGroup> GetBonusGroupApplicableAllowance();
        Task<BonusGroup> GetBonusGroupDetails(Guid BonusGroupGI);
        Task<ResponseEntity> Save(BonusGroup input, string mode);
        Task<ResponseEntity> Delete(BonusGroup model);
    }
}
