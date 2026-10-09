using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll.Reports
{
   public interface ILOPReportService
    {
        public Task<LOPReport> GetList(int Month,  int Type);
        public Task<DataSet> GetLOPReport_Excel(int Month, int Type, Filter filter);
    }
}
