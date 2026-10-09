using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank.Reports
{
    public interface IYearOfServiceReportService
    {
        Task<YearOfServiceReport> GetYearOfServiceReport( int Type, string listCategory, Filter filterdata);
        public Task<DataSet> YearOfServiceReport_Excel(int Type, string listCategory, Filter filterdata);

    }
}
