using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll.Reports
{
   public interface IWPSReportService
    {
        public Task<System.Data.DataSet> GetDetails(int SalaryPeriod, WPSReport model);
    }
}
