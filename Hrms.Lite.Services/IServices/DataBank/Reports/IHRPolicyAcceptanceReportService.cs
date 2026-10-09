using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank.Reports
{
    public interface IHRPolicyAcceptanceReportService
    {
        public Task<HRPolicyAcceptanceReport> GetPolicyAcceptanceListReport(HRPolicyAcceptanceReport model);
        public Task<DataSet> PolicyAcceptanceListReport_Excel(HRPolicyAcceptanceReport model);
        public Task<HRPolicyAcceptanceReport> GetGroupSummary(HRPolicyAcceptanceReport model);
        public Task<HrPolicy_AcceptanceReport> GetHrPolicyAcceptanceDetails(int PolicyCode, Filter filter);
        public Task<DataSet> HrPolicyAcceptance_Excel(Filter filter, int PolicyCode);
    }
}
