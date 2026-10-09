using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll.Reports
{
    public interface IPTReportService
    {
        public Task<PTReport> GetPTReport(int SalaryPeriod, int PtGroup, FilterMultiSelect filter);
        public Task<DataSet> GetPTReport_Excel(int SalaryPeriod, int PtGroup, FilterMultiSelect filter);
    }
}
