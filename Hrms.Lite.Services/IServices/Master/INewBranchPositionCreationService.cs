
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface INewBranchPositionCreationService
    {
        public Task<NewBranchPositionCreationViewModel> GetNewBranchPositionCreationList(string TabIndex,  int? PlanningPeriod = null);
         
        public Task<PositionJobDetails> GetMasterJobDetails(int PlanCode);
        public Task<ResponseEntity> SavePositionRequest(PositionRequest model);

    }
}
