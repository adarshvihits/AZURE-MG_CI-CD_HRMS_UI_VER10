using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank.Reports
{
   public interface ISalaryStructureReportService
    {
        public Task<SalaryStructureReport> GetList(int TabIndex,Filter filter);
        public Task<SalaryStructureReport> GetSalaryStructureList(int TabIndex, Filter filter);
        public Task<DataSet> GetSalaryStructureReportHeaders( int Tabindex, Filter filterdata);
        public Task<DataSet> GetSalaryStructureReportData( int Tabindex, Filter filterdata);
    }
}
