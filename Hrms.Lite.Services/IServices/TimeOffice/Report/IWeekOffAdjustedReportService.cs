using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TimeOffice.Report
{
   public  interface IWeekOffAdjustedReportService
    {
        Task<WeekOffAdjustedReport> GetWeekOffAdjustedReport(int Month, Filter Filter);
        public Task<DataSet> WeekOffAdjustedReport_Excel(int Month, Filter Filter);

        
    }
}
