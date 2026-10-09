using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank.Reports
{
    public interface IConfirmationTrackingReportService
    {
        public Task<ConfirmationTrackingReport> GetList(string fromDate, string toDate, Filter filter);
        public Task<ConfirmationTrackingReport> GetTrackingDetails(int HDcode);
        public Task<DataSet> GetList_Excel(string fromDate, string toDate, Filter filter);
    }
}
