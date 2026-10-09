using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TrainingModule;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TrainingModule
{
   public interface ITrainingCostAllotedServices
    {
        public Task<TraineeCost> GetList();
        public Task<ResponseEntity> Create(TraineeCost input);
    }
}
