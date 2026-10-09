using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll.Reports
{
  public interface ILOPPeriodwiseService
    {
        public Task<LOPPeriodwise> GetList(int fmonth, int tmonth, Filter filter);
        public Task<DataSet> GetLOPReport_Excel(int fmonth, int tmonth, Filter filter);
    }
}
