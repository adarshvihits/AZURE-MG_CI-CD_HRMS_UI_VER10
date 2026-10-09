using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll.Reports
{
   public interface ISalaryReportDraftService
    {
        public Task<SalaryReportDraft> GetList(int Month, int PayrollGrp, Filter filter);
        public Task<NetPaySplitUp> GetNetPaySplitUpDetails(Guid EmployeeGI,int SalaryPeriod,int PayrollGroup);
        public Task<System.Data.DataSet> GetSalaryReportDraft_Excel(int Month, int PayrollGroup, Filter filter);
    }
}
