using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll.Reports
{
    public interface ISalaryReportsService
    {
        public Task<SalaryIntegrityReport>GetSalaryIntegrityReportList(SalaryIntegrityReport model);
        public Task<DataSet> GetSalaryIntegrityReportList_Excel(SalaryIntegrityReport model);
        public Task<SalaryIntegrityReport> GetSalaryIntegrityReport_CheckList();
        public Task<SalaryIntegrityReport> GetSalaryIntegrityReport_Details(int MonthA, int MonthB, int EmployeeCode);
    }
}
