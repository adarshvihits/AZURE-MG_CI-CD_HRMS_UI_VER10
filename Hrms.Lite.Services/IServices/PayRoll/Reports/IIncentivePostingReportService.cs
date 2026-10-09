using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll.Reports
{
    public interface IIncentivePostingReportService
    {
        public Task<IncentivePostingReport> GetIncentivePostingReport(int Month, Filter filter);
        public Task<DataSet> GetIncentivePostingReport_Excel(int Month, Filter filter);
    }
}
