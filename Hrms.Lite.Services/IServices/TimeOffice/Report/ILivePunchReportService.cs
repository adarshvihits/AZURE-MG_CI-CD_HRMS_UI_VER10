using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TimeOffice.Report
{
    public  interface ILivePunchReportService
    {
        public Task<LivePunchReport> GetList(string Date,Filter filter);
        public Task<DataSet> LivePunchReport_Excel(Filter filter, string Date);
    }
}
