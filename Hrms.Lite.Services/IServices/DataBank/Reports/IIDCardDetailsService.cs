using Hrms.Lite.Shared;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank.Reports
{
    public interface IIDCardDetailsService
    {
        public Task<IDCardDetails> GetIDCardDetails(string FromDate, string ToDate, Filter filter);
        public Task<DataSet> GetIDCardDetails_Excel(string FromDate, string ToDate, Filter filter);
        Task<File> DownloadDoc(string EmployeeId);
    }
}
