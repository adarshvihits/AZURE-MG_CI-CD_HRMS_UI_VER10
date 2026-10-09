using Hrms.Lite.Shared.DataBank.Reports;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank.Reports
{
    public interface IEmployeeGroupSummaryReportService
    {


        public Task<EmployeeGroupSummaryReport> GetEmployeeGroupSummaryReportList(EmployeeGroupSummaryReport model);
        public Task<EmployeeGroupSummaryReport> GetPopUp(int GroupbyY);
    }
}
