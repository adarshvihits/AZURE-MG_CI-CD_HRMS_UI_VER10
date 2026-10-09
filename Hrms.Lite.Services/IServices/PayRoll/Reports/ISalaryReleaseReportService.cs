using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll.Reports
{
  public  interface ISalaryReleaseReportService
    {
        public Task<SalaryReleaseReport> GetList(int Month,int PayrollGrp, Filter filter);
        public Task<DataSet> GetSalaryReleaseReport_Excel(int Month,int PayrollGrp, Filter filter);
    }
}
