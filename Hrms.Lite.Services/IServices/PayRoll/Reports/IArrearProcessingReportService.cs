using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll.Reports
{
   public interface IArrearProcessingReportService
    {
        public Task<ArrearProcessingReport> GetArrearProcessingReport(int Month,Filter filter);
        public Task<DataSet> GetArrearProcessingReport_Excel(int Month, Filter filter);
    }
}
