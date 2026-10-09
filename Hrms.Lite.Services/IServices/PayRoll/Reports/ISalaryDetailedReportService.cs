using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll.Reports
{
    public interface ISalaryDetailedReportService
    {

        public Task<DataSet> GetSalaryDetailedHeaders(SalaryReports model,int SalaryPeriod, string allowance,string deduction);
        public Task<DataSet> GetSalaryDetailedReport(int SalaryPeriod, SalaryReports model, string allowance, string deduction);
        public Task<SalaryReports> GetList(SalaryReports model);
        public Task<DataSet> GetSalaryDetailedReport_Excel( SalaryReports model);
        public Task<SalaryReports> GetPopUp();
    }
}
