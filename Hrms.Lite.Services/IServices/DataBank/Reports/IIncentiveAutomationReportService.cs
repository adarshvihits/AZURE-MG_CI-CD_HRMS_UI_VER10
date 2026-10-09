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
    public interface IIncentiveAutomationReportService
    {
        public Task<IncentiveAutomationReport> GetList(int monthcode, Filter filter);
        public Task<DataSet> GetList_Excel(int monthcode, Filter filter);
        public Task<ResponseEntity> SaveIDBUpload(int monthcode);
    }
}
