using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll.Reports
{
    public interface IMonthlyCTCReportService
    {
        public Task<System.Data.DataSet> GetMonthlyCTCReport(int SalaryPeriodCode, PaySheet model);
    }
}
