using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll.Reports
{
    public interface ILWFReportService
    {
        public Task<LWFReport> GetList(int Month, int DeductionCode, Filter filter);
        public Task<LWFReport> GetLWFReport(int Month, int DeductionCode);
        public Task<DataSet> LWFReport_Excel(int Month, int DeductionCode, Filter Filter);

      
    }
}
