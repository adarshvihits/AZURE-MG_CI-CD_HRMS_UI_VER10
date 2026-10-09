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

    public interface IJoiningReportService
    {
        public Task<JoiningReport> GetJoiningReport(string fromDate, string toDate ,Filter filter);
        Task<DataSet> GetExcelJoiningReport(string FromDate, string ToDate,Filter filter);
        public Task<JoiningReport> GetGroupSummaryDetails(int type, string fromDate, string toDate, Filter filter);
      
    }
}
