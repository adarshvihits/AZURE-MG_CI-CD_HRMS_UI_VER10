using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TrainingModule;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TrainingModule
{
    public interface ITrainingDueListService
    {
        public Task<TrainingDueList> GetList(int trainingCode, Filter filter);
        public Task<DataSet> TrainingDueList_Excel(int trainingcode, Filter filter);
    }
}
