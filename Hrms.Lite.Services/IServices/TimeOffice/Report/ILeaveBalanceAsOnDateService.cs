using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TimeOffice.Report
{
    public interface ILeaveBalanceAsOnDateService
    {
       public Task<LeaveBalanceAsOnDate> GetList (string Date, string Type, int Leave, Filter filter);
       public Task<DataSet> LeaveBalanceAsOnDate_Excel(string Date, string Type, int Leave, Filter filter);

    }
}
