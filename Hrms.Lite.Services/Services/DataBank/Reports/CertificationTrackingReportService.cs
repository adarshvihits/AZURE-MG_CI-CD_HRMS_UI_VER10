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
    public class CertificationTrackingReportService: ICertificationTrackingReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public CertificationTrackingReportService(IServiceBase httpService)
        {
            _baseURL = "CertificationTrackingReport";
            _httpService = httpService;
        }
        public async Task<CertificationTrackingReport> GetList(string fromDate, string toDate, Filter filter)
        {
            return await _httpService.PostAsync<CertificationTrackingReport, Filter>($"{_baseURL}/GetList?fromDate={fromDate}&toDate={toDate}", filter);
        }
        public async Task<CertificationTrackingReport> GetCertificationTrackingDetails(int HDcode)
        {
            return await _httpService.GetAsync<CertificationTrackingReport>($"{_baseURL}/GetCertificationTrackingDetails?HDcode={HDcode}");
        }
        public async Task<DataSet> GetList_Excel(string fromDate, string toDate, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetList_Excel?fromDate={fromDate}&toDate={toDate}", filter);
        }
    }
}
