using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TimeOffice.Report
{
   public  interface IMonthlyWorkHourReportService
    {
        Task<WorkHourDetails> GetMonthlyWorkingHoursReport(int Month, Filter Filter);
        public Task<DataSet> GetMonthlyWorkingHoursReport_Excel(int Month, Filter Filter);

        
    }
}
