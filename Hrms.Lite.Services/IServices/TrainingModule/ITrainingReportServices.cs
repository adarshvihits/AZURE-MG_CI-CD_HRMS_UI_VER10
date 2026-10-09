using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TrainingModule;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TrainingModule
{
  public  interface ITrainingReportServices
    {
        public Task<TrainingReport> GetTrainingReportList(int TrainingCode,int FYCode);
        //public Task<TrainingReport> GetEmployeeList(int ScheduleCode, Filter filter);
    }
}
