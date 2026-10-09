using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank.Reports
{
    public interface IRenewalInformationService
    {
        public Task<RenewalInformation> RenewalInformationReport(string fromDate, string toDate);
        public Task<RenewalInformation> RenewalInformationList(int type, string fromDate, string toDate, Filter filter);
        public Task<DataSet> GetRenewalInformationList_Excel( int Type, string FromDate, string ToDate, Filter filter);


    }
}
