using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TrainingModule;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TrainingModule
{
    public interface ITRAININGCOMPLETIONService
    {
        Task<TrainingCompletion> GetList(int StatusCode, int FinancialYearCode);
        Task<TrainingCompletion> GetCostList(int ScheduleCode);

        Task<ResponseEntity> Create(TrainingCompletion input);
    }
}
