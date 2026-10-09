using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll.Reports
{
    public interface IESISummaryReportService
    {
        public Task<ESISummaryReport> GetESISummaryReport(int MonthCode, int ESIGroupCode,Filter filter);
        public Task<DataSet> GetESISummaryReport_Excel( int Month, int ESIGroup, Filter filter);

    }
}
