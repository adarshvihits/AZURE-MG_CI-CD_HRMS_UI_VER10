using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank.Reports
{
    public class JoiningReportService : IJoiningReportService
    {

        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public JoiningReportService(IServiceBase httpService)
       {
        _baseURL = "JoiningAndSeperationReport";
        _httpService = httpService;
       }

        public async Task<JoiningReport> GetJoiningReport(string fromDate, string toDate,Filter filter)
        {
            return await _httpService.PostAsync<JoiningReport,Filter>($"{_baseURL}/GetJoiningReport?fromDate={fromDate}&toDate={toDate}", filter);
        }
        public async Task<DataSet> GetExcelJoiningReport(string fromDate, string toDate, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetExcelJoiningReport?fromDate={fromDate}&toDate={toDate}", filter);
        }
        public async Task<JoiningReport> GetGroupSummaryDetails(int type, string fromDate, string toDate, Filter filter)
        {
            return await _httpService.PostAsync<JoiningReport, Filter>($"{_baseURL}/GetJoiningGroupingReport?type=" + type + "&fromDate=" + fromDate + "&toDate=" + toDate, filter);

        }

    }
}
