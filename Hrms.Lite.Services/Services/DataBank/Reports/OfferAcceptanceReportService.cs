using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank.Reports
{
    public class OfferAcceptanceReportService: IOfferAcceptanceReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public OfferAcceptanceReportService(IServiceBase httpService)
        {
            _baseURL = "OfferAcceptanceReport";
            _httpService = httpService;
        }
        public async Task<OfferAcceptanceReport> GetOfferAcceptanceReportList(int TabIndex, Filter Filter)
        {
            return await _httpService.PostAsync<OfferAcceptanceReport, Filter>($"{_baseURL}/GetOfferAcceptanceReportList?TabIndex={TabIndex}", Filter);
        }
        public async Task<DataSet> GetOfferAcceptanceReportExcelList(int Tabindex, Filter Filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetOfferAcceptanceReportExcelList?Tabindex={Tabindex}", Filter);
        }
    }
}
