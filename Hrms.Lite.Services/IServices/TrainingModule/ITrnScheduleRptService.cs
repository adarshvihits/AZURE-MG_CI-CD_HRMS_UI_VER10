using Hrms.Lite.Shared.TrainingModule;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TrainingModule
{

   public interface ITrnScheduleRptService
    {
        Task<DataSet> GetTrnScheduleRptList( int FinancialYearCode, int Category);
       Task<TrainingSchedule> GetDashboardCountList(int FinancialYearCode);
      
    }
}
