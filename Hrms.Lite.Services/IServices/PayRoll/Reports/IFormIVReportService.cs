using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll.Reports
{
    public interface IFormIVReportService
    {
        public Task<FormIVReport> GetFormIVReport(Filter filter,int Month);
        public Task<DataSet> FormIVReport_Excel(int Month, Filter Filter);


    }
}
