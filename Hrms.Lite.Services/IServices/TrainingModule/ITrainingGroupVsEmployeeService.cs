using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TrainingModule;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TrainingModule
{
    public interface ITrainingGroupVsEmployeeService
    {
        public Task<TrainingGroupVsEmployee> GetEmployeeList( Filter filter);
        public Task<ResponseEntity> Save(TrainingGroupVsEmployee input);
        public Task<DataSet> GetEmployeeList_Excel(Filter filter);
        public Task<ResponseEntity> TrainingGroupVsEmployeeBatchUpdate(Filter filter, int TrainingGroupCode);
    }
}
