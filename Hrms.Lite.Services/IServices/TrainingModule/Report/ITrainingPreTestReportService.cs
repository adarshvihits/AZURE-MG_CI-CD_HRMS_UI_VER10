using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TrainingModule.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
namespace Hrms.Lite.Services.IServices.TrainingModule.Report
{
    public interface ITrainingPreTestReportService
    {
        public Task<TrainingPreTestReport> GetTrainingPreTestReportList(int Training, Filter filter);
        public Task<DataSet> GetTrainingPreTestReport_Excel(Filter filter, int Training);

    }
}
