using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll.Reports
{
 public   interface   IPreFullandFinalSettlementReportService
    {
        public Task<PreFullandFinalSettlement> FandFDetailedReport(int Month, Filter filter);
        public Task<DataSet> FandFDetailedReport_EXCEL(int Month, Filter filter);
    }
}
