using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank.Reports
{
    public interface IEConsoleSubmissionReportService
    {
        public Task<EConsoleSubmissionReport> GetList(int ConsoleType, Filter filter);
        public Task<DataSet> EConsoleSubmissionreport_Excel(int TypeCode,Filter filter);


    }
}
