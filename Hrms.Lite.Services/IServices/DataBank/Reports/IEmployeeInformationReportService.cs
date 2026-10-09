using Hrms.Lite.Shared.DataBank.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank.Reports
{
    public interface IEmployeeInformationReportService
    {
        Task<System.Data.DataSet> GetDetails(EmployeeInformationReport model);
        public Task<EmployeeInformationReport> GetPersonalDetailsDetailedReport(EmployeeInformationReport model);

        public Task<DataSet> GetPersonalDetailsDetailedReport_Excel( EmployeeInformationReport model);
    }
}
