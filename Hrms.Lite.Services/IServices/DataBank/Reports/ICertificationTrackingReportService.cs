using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
namespace Hrms.Lite.Services.IServices.DataBank.Reports
{
    public interface ICertificationTrackingReportService
    {
        public Task<CertificationTrackingReport> GetList(string fromDate, string toDate, Filter filter);
        public Task<CertificationTrackingReport> GetCertificationTrackingDetails(int HDcode);
        public Task<DataSet> GetList_Excel(string fromDate, string toDate, Filter filter);
    }
}
