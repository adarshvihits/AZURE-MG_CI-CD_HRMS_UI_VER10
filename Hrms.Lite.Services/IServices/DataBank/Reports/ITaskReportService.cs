using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank.Reports
{
    public interface ITaskReportService
    {

        public Task<EmployeeTaskReport> TaskReportList(string Status, Filter filter);
        public Task<DataSet> GetTaskReport_Excel(string Status, Filter filter);
        public Task<EmployeeTaskReport> GetTaskReportList(string Status, Filter filter);
        public Task<DataSet> TaskReport_Excel(string Status, Filter filter);
        public Task<EmployeeTaskReport> GetActionDetails(int TaskCode);
    }
}
