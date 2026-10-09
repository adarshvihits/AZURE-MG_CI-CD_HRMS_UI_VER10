using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll.Reports
{
    public interface IPFSummaryReportService
    {
        public Task<PFSummaryReport> GetList(int Month,int PFGroup,Filter filter);
        public Task<Url> GetUrl();
        public Task<Url> GetOfferLetter(string Type);
        public Task<System.Data.DataSet> GetPFSummaryReport_Excel( int Month, int PFGroup, Filter filter);
    }
}
