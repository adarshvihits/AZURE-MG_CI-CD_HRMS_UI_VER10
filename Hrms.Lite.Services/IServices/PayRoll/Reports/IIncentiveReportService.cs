using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.PayRoll.Reports
{
    public interface IIncentiveReportService
    {
        public Task<DataSet> GetDetails(int SalaryPeriod, IncentiveReport model);
    }
}
