
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank.Reports
{
    public interface IDealerUnderTakingReportService
    {
        public Task<DealerUnderTakingReport> GetList(int TabIndex, Filter model);
        public Task<DealerUnderTakingReport> GetDealerUnderTakingDetails(Guid EmployeeGI, string Type);
        public Task<DataSet> GetList_Excel(int TabIndex, Filter model);
    }
}
