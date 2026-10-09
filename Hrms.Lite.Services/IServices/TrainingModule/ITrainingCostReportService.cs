using Hrms.Lite.Shared.TrainingModule;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TrainingModule
{
   public interface ITrainingCostReportService
    {
        public Task<TrainingCostReport> GetReportList(int FYCode, int Category);
    }
}
