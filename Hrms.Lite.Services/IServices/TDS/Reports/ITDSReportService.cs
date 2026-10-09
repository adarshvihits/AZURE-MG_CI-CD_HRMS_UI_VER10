using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TDS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TDS.Reports
{
  public  interface ITDSReportService
    {
        public Task<TDSProcessing> GetTDSReport(Filter filter,int Month);
        public Task<DataSet> GetTDSReport_Export(Filter filter, int Month);
    }
}
