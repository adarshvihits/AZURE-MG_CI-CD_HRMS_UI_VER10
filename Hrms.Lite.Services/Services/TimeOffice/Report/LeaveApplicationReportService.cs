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
    public class LeaveApplicationReportService : ILeaveApplicationReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public LeaveApplicationReportService(IServiceBase httpService)
        {
            _baseURL = "LeaveApplicationReport";
            _httpService = httpService;
        }
        public async Task<LeaveApplicationReport> GetLeaveApplicationList(int currntLp, string TabIndex, string FromDate, string ToDate,string Type, Filter filter)
        {
            return await _httpService.PostAsync<LeaveApplicationReport, Filter>($"{_baseURL}/LeaveApplicationReportList?LeavePeriod={currntLp}&TabIndex={TabIndex}&FromDate={FromDate}&ToDate={ToDate}&Type={Type}", filter);
        }

        public async Task<LeaveApplicationReport> GetTrackerDetails(Guid EmployeeGI, string LeaveType)
        {
            return await _httpService.GetAsync<LeaveApplicationReport>($"{_baseURL}/GetTrackerDetails?LeaveApplicationGI={EmployeeGI}&Type={LeaveType}");
        }

       public async Task<DataSet> LeaveApplicationReport_Excel(int LeavePeriod, string TabIndex, string FromDate, string ToDate, string Type, Filter filterdata)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/LeaveApplicationReport_Excel?LeavePeriod={LeavePeriod}&TabIndex={TabIndex}&FromDate={FromDate}&ToDate={ToDate}&Type={Type}", filterdata);

        }
    }
}
