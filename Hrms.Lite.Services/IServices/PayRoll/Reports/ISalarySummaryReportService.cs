using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll.Reports
{
    public interface ISalarySummaryReportService
    {
        public Task<SalaryReports> GetList(int salCode,Filter filter);
        public Task<System.Data.DataSet> GetSalarySummary_Excel( int SalaryPeriodCode, Filter filter);
        public Task<SalaryReports> GetGroupSummaryDetails(int firstgroupById, int salCode, Filter filter);
    }
}
