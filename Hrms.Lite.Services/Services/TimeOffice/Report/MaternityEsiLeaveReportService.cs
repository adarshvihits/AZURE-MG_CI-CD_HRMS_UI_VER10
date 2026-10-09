using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TimeOffice.Report;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.TimeOffice.Report
{
    public class MaternityEsiLeaveReportService : IMaternityEsiLeaveReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public MaternityEsiLeaveReportService(IServiceBase httpService)
        {
            _baseURL = "MaternityEsiLeaveReport";
            _httpService = httpService;
        }
        public async Task<MaternityEsiLeaveReport> GetMaternityEsiLeaveReport(int LeaveCode,Filter filterdata)
        {
            return await _httpService.PostAsync<MaternityEsiLeaveReport, Filter>($"{_baseURL}/GetMaternityEsiLeaveReport?LeaveCode={LeaveCode}", filterdata);

        }

        public async Task<DataSet> GetMaternityEsiLeaveReport_Excel(int LeaveCode,Filter filterdata)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetMaternityEsiLeaveReport_Excel?LeaveCode={LeaveCode}", filterdata);
        }
    }
}
