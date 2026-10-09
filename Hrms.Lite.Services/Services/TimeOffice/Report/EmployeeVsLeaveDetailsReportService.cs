using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TimeOffice.Report;
using Hrms.Lite.Shared.TimeOffice.Report;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.TimeOffice.Report
{
    public class EmployeeVsLeaveDetailsReportService : IEmployeeVsLeaveDetailsReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public EmployeeVsLeaveDetailsReportService(IServiceBase httpService)
        {
            _baseURL = "LeaveRegister";
            _httpService = httpService;
        }
        public async Task<LeaveRegister> GetEmployeeVsLeaveReports(int LeavePeriod, int EmployeeCode)
        {
            return await _httpService.GetAsync<LeaveRegister>($"{_baseURL}/GetEmployeeVsLeaveReports?LeavePeriod={LeavePeriod}&EmployeeCode={EmployeeCode}");
        }
        public async Task<LeaveRegister> GetEmployeeVsLeaveReportDetails(int LeavePeriod, int EmployeeCode, int LeaveCode)
        {
            return await _httpService.GetAsync<LeaveRegister>($"{_baseURL}/GetEmployeeVsLeaveReportDetails?LeavePeriod={LeavePeriod}&EmployeeCode={EmployeeCode}&LeaveCode={LeaveCode}");
        }
        public Task<System.Data.DataSet> GetEmployeeVsLeaveReports_Excel(int LeavePeriod, int EmployeeCode)
        {
            return _httpService.GetAsync<System.Data.DataSet>($"{_baseURL}/GetEmployeeVsLeaveReports_Excel?LeavePeriod={LeavePeriod}&EmployeeCode={EmployeeCode}");
        }
        public Task<System.Data.DataSet> GetEmployeeVsLeaveDetailsReports_Excel(int LeavePeriod, int EmployeeCode, int LeaveCode)
        {
            return _httpService.GetAsync<System.Data.DataSet>($"{_baseURL}/GetEmployeeVsLeaveDetailsReports_Excel?LeavePeriod={LeavePeriod}&EmployeeCode={EmployeeCode}&LeaveCode={LeaveCode}");
        }
    }
}
