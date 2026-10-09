using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank.Reports
{
    public interface IRetirementDueReportService
    {
        public Task<JobTransition> GetRetirementDueReport(int Tabindex, string FromDate, string ToDate, Filter filter);
        public Task<DataSet> GetRetirementDueReport_Excel(int Tabindex, string FromDate, string ToDate, Filter filter);
    }
}
