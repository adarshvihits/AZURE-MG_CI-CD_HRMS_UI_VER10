using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank.Reports
{
    public interface IPeriodicalEmployeelistService
    {
        public Task<PeriodicalEmployeelist> GetPeriodicalEmployeelistDetails(string FromDate, string ToDate, Filter filter);
        public Task<DataSet> PeriodicalEmployeelist_Excel(string FromDate, string ToDate, Filter filter);
    }
}
