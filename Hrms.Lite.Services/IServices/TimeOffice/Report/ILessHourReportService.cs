using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TimeOffice.Report
{
    public interface ILessHourReportService
    {
        public Task<LessHourReport> LessHourReportList(string FromDate, string ToDate, Filter filter);
        public Task<DataSet> GetLessHourReportList_Excel(string FromDate, string ToDate, Filter filter);
    }
}
