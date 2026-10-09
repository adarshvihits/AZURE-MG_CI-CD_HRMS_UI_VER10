using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TrainingModule;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TrainingModule
{
  public interface ITraineeCostService
    {
        public Task<TraineeCost> GetScheduleList(int ScheduleCode, Filter filter);
        public Task<TraineeCost> GetCostDetails(Guid empGI);
        public Task<TraineeCost> Batchupdation(int ScheduleCode);
        public Task<ResponseEntity> Save(int ScheduleCode, TraineeCost model);
        public Task<ResponseEntity> BatchSave(int ScheduleCode, TraineeCost model);
    }
}
