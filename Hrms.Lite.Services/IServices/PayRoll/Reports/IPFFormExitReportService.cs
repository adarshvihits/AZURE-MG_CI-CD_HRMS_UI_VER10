using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll.Reports
{
    public interface IPFFormExitReportService
    {
        public Task<PFExitReport> GetEmployeeList(int SalaryPeriod,int PfGroup,Filter filter);
        public Task<DataSet> GetEmployeeList_Excel(int SalaryPeriod, int PfGroup, Filter filter);

    }
}
