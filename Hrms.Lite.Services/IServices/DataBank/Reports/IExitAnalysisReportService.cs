using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank.Reports
{
    public interface IExitAnalysisReportService
    {
        public Task<ExitAnalysisReport> GetList(string FromDate, string ToDate, string Type, Filter filter);

        public Task<ExitAnalysisReport> GetPopupList( string FromDate, string ToDate, Guid GI, string Type);

        public Task<DataSet> ExitAnalysisReport_Excel(Filter filter);

    }
}
