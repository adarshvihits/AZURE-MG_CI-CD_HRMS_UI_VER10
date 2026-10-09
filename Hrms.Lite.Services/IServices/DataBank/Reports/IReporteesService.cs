
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank.Reports
{
  public  interface IReporteesService
    {
        public Task<Reportees> GetReporteesList(int Type,int Level, int Reporter, Filter filter);
        public Task<DataSet> GetExcelList(int Type, int Level, int Reporter, Filter filter);
    }
}
