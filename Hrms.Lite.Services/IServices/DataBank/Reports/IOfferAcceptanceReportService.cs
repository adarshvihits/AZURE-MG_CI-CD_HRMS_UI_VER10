using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank.Reports
{
    public interface IOfferAcceptanceReportService
    {
        public Task<OfferAcceptanceReport> GetOfferAcceptanceReportList(int TabIndex, Filter Filter);
        public Task<DataSet> GetOfferAcceptanceReportExcelList(int TabIndex, Filter Filter);
    }
}
