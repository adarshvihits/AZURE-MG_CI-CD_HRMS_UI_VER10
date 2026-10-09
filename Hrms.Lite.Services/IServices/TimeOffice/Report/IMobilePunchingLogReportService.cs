using Hrms.Lite.Shared.TimeOffice.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TimeOffice.Report
{
    public interface IMobilePunchingLogReportService
    {
        Task<ReaderLogReport> GetMobileLogListReport(ReaderLogReport ReaderLog);
        Task<DataSet> GetMobileLogListReport_Excel(ReaderLogReport ReaderLog);
    }
}
