using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank.Reports
{
   public interface ISalaryPerDayReportService
    {
        public Task<SalaryPerDayReport> GetList(SalaryPerDayReport model);
        public Task<DataSet> GetList_Excel(SalaryPerDayReport model);

    }
}
