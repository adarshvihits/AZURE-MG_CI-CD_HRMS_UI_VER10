using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank.Reports
{
    public interface IOnboardingChecklistReportService
    {
        public Task<OnboardingChecklistReport> GetOnboardingChecklistReport(string FromDate, string ToDate, Filter filter);
        public Task<DataSet> GetOnboardingChecklistReport_Excel(string FromDate, string ToDate, Filter filter);
        public Task<OnboardingChecklistReport> GetOnboardingChecklistDetails(int EmployeeCode);
        public Task<DataSet> GetOnboardingChecklistDetails_Excel(int EmployeeCode);

    }
}
