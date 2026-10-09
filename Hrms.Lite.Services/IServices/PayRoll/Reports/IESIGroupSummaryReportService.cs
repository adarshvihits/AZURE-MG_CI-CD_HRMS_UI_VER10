using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll.Reports
{
    public interface IESIGroupSummaryReportService
    {
        public Task<ESIGroupSummaryReport> GetList(int Month, int PFGroup, Filter filter);
        public Task<System.Data.DataSet> GetESIGroupSummaryReport_Excel(int Month, int ESIGroup, Filter filter);
    }
}
