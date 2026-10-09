using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank.Reports
{
    public interface IReplacementOrNewExpReportService
    {
        public Task<ReplacementOrNewExpReport> GetList( Filter filter);
        public Task<DataSet> GetList_Excel( Filter Filter);
    }
}
