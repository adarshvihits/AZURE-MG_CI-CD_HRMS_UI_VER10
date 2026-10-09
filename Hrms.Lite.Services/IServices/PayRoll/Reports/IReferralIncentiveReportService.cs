using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll.Reports
{
    public interface IReferralIncentiveReportService
    {
        public Task<ReferralIncentiveReport> GetList(FilterMultiSelect filter, int MonthCode);

        public Task<DataSet> GetList_Excel(int MonthCode);
    }
}
