using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll.Reports
{
    public interface IPFGroupSummaryReportService
    {
        public Task<PFGroupSummaryReport> GetList(int Month, int PFGroup, Filter filter);
        public Task<System.Data.DataSet> GetPFGroupSummaryReport_Excel(int Month, int PFGroup, Filter filter);
    }
}
