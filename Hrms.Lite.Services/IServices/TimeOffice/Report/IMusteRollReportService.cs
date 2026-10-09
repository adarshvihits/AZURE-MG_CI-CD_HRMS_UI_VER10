using Hrms.Lite.Shared.TimeOffice.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TimeOffice.Report
{
  public  interface IMusteRollReportService
    {

        Task<MusterRollReport> GetMusterRollReport(MusterRollReport model);
        Task<DataSet> GetMusterRollReport_Excel(MusterRollReport model);
        Task<DataSet> GetMusterRollReportDynamic(MusterRollReport model);
        Task<DataSet> GetMusterRollReportHeaders(MusterRollReport model);

    }
}
