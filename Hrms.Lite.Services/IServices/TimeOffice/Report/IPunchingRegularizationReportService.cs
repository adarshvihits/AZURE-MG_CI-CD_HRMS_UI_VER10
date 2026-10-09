using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TimeOffice.Report
{
   public  interface IPunchingRegularizationReportService
    {
        public Task<PunchingRegularizationReport> GetRegularizationList(string TabIndex, int Month, int Reason, int LeavePeriod,Filter filter);
        public Task<DataSet> GetRegularizationList_Excel(string TabIndex, int Month, int Reason, int LeavePeriod, Filter filter);
        //   public Task<PunchingRegularizationReport> GetRegularizationListHeader(PunchingRegularizationReport Model);
    }
}
