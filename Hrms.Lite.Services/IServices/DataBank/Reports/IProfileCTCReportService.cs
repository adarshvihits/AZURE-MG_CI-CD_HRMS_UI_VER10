using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank.Reports
{
    public interface IProfileCTCReportService
    {
        public Task<ProfileCTCReport> GetReportList(int SalaryPeriod, Filter filter);
        public Task<System.Data.DataSet> GetProfileCTCReport_Excel( int SalaryPeriodCode, Filter filter);
    }
}
