using Hrms.Lite.Shared.DataBank.DTO;

using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank.Reports
{
  public interface IAbscondingReportService
    {
        public Task<AbscondingReport> GetAbscondingReport(int DayCode, Filter filter);
        public Task<DataSet> GetAbscondingReport_Excel( Filter filter,int DayCode);
    }
}
