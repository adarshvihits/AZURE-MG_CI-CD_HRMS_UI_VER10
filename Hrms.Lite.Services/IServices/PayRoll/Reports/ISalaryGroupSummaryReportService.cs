using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll.Reports
{
  public  interface ISalaryGroupSummaryReportService
    {


        public Task<System.Data.DataSet> GetDetails(int SalaryPeriod, int GroupBy, SalaryGroupSummaryReport model);

    }
}
