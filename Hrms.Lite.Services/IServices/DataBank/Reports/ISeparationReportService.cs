using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank.Reports
{
    public interface ISeparationReportService
    {
        public Task<SeparationReport> GetSeparationReport(string fromDate, string toDate, Filter filter);

        public Task<System.Data.DataSet> GetSeparationReport_Excel(string FromDate, string ToDate, Filter filter);
        public Task<SeparationReport> GetSeparationGroupSummary(int type, string fromDate, string toDate);
    }
}
