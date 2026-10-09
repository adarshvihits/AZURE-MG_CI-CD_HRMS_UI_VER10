using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank.Reports
{
    public interface IEmployeeTransferReportService
    {
        public Task<EmployeeTransferReport> GetEmployeeTransferReport(EmployeeTransferReport model);
        public Task<System.Data.DataSet> GetEmployeeTransferReport_Excel(EmployeeTransferReport model);
    }
}
