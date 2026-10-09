using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll.Reports
{
  public interface IShiftVarianceReportService
    {
        public Task<ShiftVarianceReport> GetShiftVarianceReport(string Date, Filter filter);
        public Task<DataSet> GetShiftVarianceReport_Excel(string Date,Filter filter);
    
    }
}
