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
    public class DueClearenceTrackingReportService : IDueClearenceTrackingReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public DueClearenceTrackingReportService(IServiceBase httpService)
        {
            _baseURL = "DueClearenceTrackingReport";
            _httpService = httpService;
        }
        public async Task<DueClearenceTrackingReport> GetList(Filter filter)
        {
            return await _httpService.PostAsync<DueClearenceTrackingReport, Filter>($"{_baseURL}/GetList?",filter);
        }
        public async Task<DataSet> DueClearenceTrackingReport_Excel(string FromDate, string ToDate, Filter Filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetDueClearenceTrackingReport_Excel?FromDate={FromDate}&ToDate={ToDate}", Filter);

        }
    }
}
