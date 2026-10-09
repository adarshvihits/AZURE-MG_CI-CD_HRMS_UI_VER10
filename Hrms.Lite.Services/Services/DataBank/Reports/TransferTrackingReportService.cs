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
    public class TransferTrackingReportService: ITransferTrackingReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public TransferTrackingReportService(IServiceBase httpService)
        {
            _baseURL = "TransferTrackingReport";
            _httpService = httpService;
        }
        public async Task<TransferTrackingReport> GetList(string fromDate, string toDate, Filter filter)
        {
            return await _httpService.PostAsync<TransferTrackingReport, Filter>($"{_baseURL}/GetList?fromDate={fromDate}&toDate={toDate}", filter);
        }
        public async Task<TransferTrackingReport> GetTrackingDetails(int HDcode)
        {
            return await _httpService.GetAsync<TransferTrackingReport>($"{_baseURL}/GetTrackingDetails?HDcode={HDcode}");
        }
        public async Task<DataSet> GetList_Excel(string fromDate, string toDate, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetList_Excel?fromDate={fromDate}&toDate={toDate}", filter);
        }
    }
}
