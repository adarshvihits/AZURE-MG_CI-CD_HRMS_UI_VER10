using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank.Reports
{
    public interface IInductionTrainingDetailedReportService
    {
        public Task<InductionTraining> GetList(string From, string To, Filter filter);
        public Task<DataSet> GetInductionExcel(string fromDate, string toDate, Filter filter);
    }
}
