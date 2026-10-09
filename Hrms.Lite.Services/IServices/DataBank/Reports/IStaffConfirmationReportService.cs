using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank.Reports
{
    public interface IStaffConfirmationReportService
    {
        Task<StaffConfirmationReport> GetStaffConfirmationList(int Month);
        public Task<StaffConfirmationReport> GetStaffConfirmationReportDetails(Guid EmployeeGI, int Month);
        public Task<DataSet> GetStaffConfirmationReport_Excel(int Month, Filter filter);

        public Task<DataSet> GetStaffConfirmationReportDetails_Excel(Guid EmployeeGI, int Month);
    }
}
