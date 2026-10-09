using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
   public interface IRewardsService
    {
        public Task<Rewards> GetList(string TabIndex);
        public Task<Rewards> GetRewardsDetails(Guid RewardGI, Guid LogGI, string type);
        public Task<List<RewardVsDepartment>> GetDepartmentGridFill();
        public Task<ResponseEntity> Create(Rewards input);
        public Task<ResponseEntity> ApproveOrReject(Rewards model);
        public Task<ResponseEntity> Delete(Rewards model);

        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);
    }
}
