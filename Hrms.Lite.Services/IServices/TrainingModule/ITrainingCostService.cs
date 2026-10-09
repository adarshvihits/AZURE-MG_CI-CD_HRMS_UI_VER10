using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TrainingModule;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TrainingModule
{
   public interface ITrainingCostService
    {
        public Task<TrainingCost> GetList(string TabIndex);
        public Task<ResponseEntity> Create(TrainingCost input);
        public Task<TrainingCost> GetDetails(Guid TrainingCostHeadGI, Guid LogGI, string type);
        public Task<ResponseEntity> ApproveOrReject(TrainingCost model);
        public Task<ResponseEntity> Delete(TrainingCost model);
    }
}
