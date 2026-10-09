using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IRewardReferingService
    {
        public Task<RewardRefering> GetList(string TabIndex);
        public Task<ResponseEntity> Create(RewardRefering input);
        public Task<RewardRefering> GetDetails(Guid RewardsReferTypeGI, Guid LogGI, string type);
        public Task<ResponseEntity> Delete(RewardRefering model);
        public Task<ResponseEntity> ApproveOrReject(RewardRefering model);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);
    }
}
