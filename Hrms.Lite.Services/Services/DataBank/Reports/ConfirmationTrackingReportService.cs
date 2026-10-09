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
    public class ConfirmationTrackingReportService: IConfirmationTrackingReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public ConfirmationTrackingReportService(IServiceBase httpService)
        {
            _baseURL = "ConfirmationTrackingReport";
            _httpService = httpService;
        }
        public async Task<ConfirmationTrackingReport> GetList(string fromDate, string toDate, Filter filter)
        {
            return await _httpService.PostAsync<ConfirmationTrackingReport, Filter>($"{_baseURL}/GetList?fromDate={fromDate}&toDate={toDate}", filter);
        }
        public async Task<ConfirmationTrackingReport> GetTrackingDetails(int HDcode)
        {
            return await _httpService.GetAsync<ConfirmationTrackingReport>($"{_baseURL}/GetTrackingDetails?HDcode={HDcode}");
        }
        public async Task<DataSet> GetList_Excel(string fromDate, string toDate, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetList_Excel?fromDate={fromDate}&toDate={toDate}", filter);
        }
    }
}
