using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TimeOffice;
using Hrms.Lite.Shared;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using Hrms.Lite.Shared.TimeOffice.Generic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.TimeOffice
{
   public class LeaveApplicationService: ILeaveApplicationService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public LeaveApplicationService(IServiceBase httpService)
        {
            _baseURL = "LeaveApplication";
            _httpService = httpService;
        }
        public async Task<LeaveApplication> LeaveApplicationEmployeeList(Filter filter)
        {
            return await _httpService.PostAsync<LeaveApplication, Filter>($"{_baseURL}/LeaveApplicationEmployeeList", filter);
        }
        public async Task<List<Shared.TimeOffice.Generic.LeaveBalance>> GetLeaveBalance(int leavePeriodCode,int employeeCode)
        {
            return await _httpService.GetAsync<List<Shared.TimeOffice.Generic.LeaveBalance>>($"{_baseURL}/GetLeaveBalance?leavePeriodCode={leavePeriodCode}&TeamMemberEmployeeCode={employeeCode}");
        }


        public async Task<LeaveDetails> GetLeaveDayWiseDetails(LeaveApplication model, Guid EmployeeGI)
        {
            return await _httpService.PostAsync<LeaveDetails, LeaveApplication>($"{_baseURL}/DayWiseDetails?EmployeeGI={EmployeeGI}", model);


        }
        public async Task<ResponseEntity> SaveLeave(LeaveApplication model)
        {
            return await _httpService.PostAsync<ResponseEntity, LeaveApplication>($"{_baseURL}/SaveHRMSLeave", model);
        }
        public async Task<LeaveApplication> GetLeaveApplicationList(int LeavePeriodCode, int LeaveType, string TabIndex,Guid EmployeeGI)
        {
            return await _httpService.GetAsync<LeaveApplication>($"{_baseURL}/GetLeaveApplicationList?LeavePeriodCode={LeavePeriodCode}&LeaveType={LeaveType}&TabIndex={TabIndex}&EmployeeGI={EmployeeGI}");
        }
        public async Task<LeaveApplication> GetTrackerDetails(Guid LeaveApplicationGI)
        {
            return await _httpService.GetAsync<LeaveApplication>($"{_baseURL}/GetTrackerDetails?LeaveApplicationGI={LeaveApplicationGI}");
        }
        public async Task<DataSet> LeaveApplication_Excel(Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/LeaveApplicationEmployeeList_Excel", filter);

        }
        public async Task<File> DownloadDoc(Guid RequestGI,string LeaveTypeName)
        {
            return await _httpService.GetAsync<File>($"{_baseURL}/DownloadDoc?RequestGI={RequestGI}&LeaveTypeName={LeaveTypeName}");
        }
    }
}
