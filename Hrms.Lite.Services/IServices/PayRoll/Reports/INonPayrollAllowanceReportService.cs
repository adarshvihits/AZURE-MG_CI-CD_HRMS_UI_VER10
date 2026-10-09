using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll.Reports
{
    public interface INonPayrollAllowanceReportService
    {
        public Task<SalaryReports> GetList(SalaryReports model);
        public Task<SalaryReports> GetPopUp();
        public Task<DataSet> GetMonthlyGratuityDetailedReport_Excel(SalaryReports model);
       public  Task<DataSet> GetNonPayrollDetailedReportHeader(int SalaryPeriodCode, SalaryReports model);
        public Task<DataSet> GetNonPayrollDetailedReportData(int SalaryPeriodCode, SalaryReports model);


        //------------non-payroll allowanace draft------------------//
        public Task<DataSet> GetNonPayrollDetailedReportDraftData(int SalaryPeriodCode, SalaryReports model);
        public Task<SalaryReports> GetDraftList(SalaryReports model);
    }
}
