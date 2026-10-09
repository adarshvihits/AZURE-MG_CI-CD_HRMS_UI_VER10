using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll.Reports
{
  public  interface IForm12LabourReportService
    {

        public Task<System.Data.DataSet> GetDetails(int SalaryPeriod, Form12LabourReport model);
    }
}
