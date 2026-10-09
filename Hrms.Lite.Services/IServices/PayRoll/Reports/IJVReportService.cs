using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll.Reports
{
   public interface IJVReportService
    {
        public Task<System.Data.DataSet> GetJVExportToExcelDetails(JVReport model);
        public Task<System.Data.DataSet> GetERPJVExportToExcelDetails(JVReport model);
    }
}
