using Hrms.Lite.Shared.TimeOffice.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TimeOffice.Report
{
    public interface IReaderLogReportService
    {
        public Task<ReaderLogReport> GetReaderLogListReport(ReaderLogReport model);
             public Task<DataSet> GetReaderLogListReport_Excel(ReaderLogReport model);
    }
}
