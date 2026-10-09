
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;


namespace Hrms.Lite.Services.IServices.TimeOffice.Report
{
    public interface IDailyPerformanceService
    {
        Task<DailyPerformance> DailyPerformanceEmployeeList(string Date, int NoOfPunch, Filter filter);
        Task<DataSet> GetDailyPerformanceList_Excel(string Date, int NoOfPunch, Filter filter);
        public Task<DailyPerformance> GetDetails(Guid EmployeeGI, string Date);

    }
}
