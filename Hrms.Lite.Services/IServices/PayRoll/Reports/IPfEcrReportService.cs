using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll.Reports
{
    public interface IPfEcrReportService
    {
        public Task<PfEcrReport> GetList(int Month, int PFGroup,int format, Filter filter);
        public Task<DataSet> GetPfEcrReport_Excel( int Month, int PFGroup, Filter filter, int Type);
    }
}
