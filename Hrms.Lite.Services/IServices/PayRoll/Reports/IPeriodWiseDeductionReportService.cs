using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll.Reports
{
    public interface IPeriodWiseDeductionReportService
    {
        public Task<DataSet> GetPeriodicDeductionReport(Filter filter, int Deduction, int MonthFrom, int MonthTo);
        public Task<DataSet> GetPeriodicDeductionReport_Excel(Filter filter, int Deduction, int MonthFrom, int MonthTo);
    }
}
