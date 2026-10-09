using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll.Reports
{
   public  interface IESIReportService
    {
        public Task<EsiEcrReport> GetReportList(int MonthCode, int EsiGroup, Filter filter);

        public Task<DataSet> GetEsiEcrReport_Excel( int Month, int ESIGroup, Filter filter);
    }
}
