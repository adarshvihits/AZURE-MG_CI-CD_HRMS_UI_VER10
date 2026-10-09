using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TrainingModule.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TrainingModule.Report
{
    public interface  ITrainingPostTestReportService
    {
        public Task<TrainingPostTestReport> GetTrainingPostTestReportList(int Code, Filter filter);
        public Task<DataSet> GetTrainingPostTestReportList_Excel(Filter filter, int Training);
    }
}
