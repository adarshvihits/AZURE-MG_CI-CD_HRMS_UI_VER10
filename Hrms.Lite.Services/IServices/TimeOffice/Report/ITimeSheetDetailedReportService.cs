using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TimeOffice.Report
{
   public interface ITimeSheetDetailedReportService
    {
        public Task<TimeSheetEntry> GetList(string From, string To, Filter filter);
        public Task<DataSet> GetTimeSheetExcel(string fromDate, string toDate, Filter filter);
    }
}
