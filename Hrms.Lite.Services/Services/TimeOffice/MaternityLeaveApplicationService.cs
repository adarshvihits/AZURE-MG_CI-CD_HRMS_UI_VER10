using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TimeOffice;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.TimeOffice;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.TimeOffice
{
    public class MaternityLeaveApplicationService : IMaternityLeaveApplicationService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public MaternityLeaveApplicationService(IServiceBase httpService)
        {
            _baseURL = "MaternityLeaveApplication";
            _httpService = httpService;
        }
      public  async Task<MaternityLeaveApplication> GetMaternityLeaveApplicationEmployeeList(Filter filter)
        {
            return await _httpService.PostAsync<MaternityLeaveApplication, Filter>($"{_baseURL}/GetMaternityLeaveApplicationEmployeeList", filter);

        }

        public async Task<DataSet> GetMaternityLeaveApplicationEmployeeList_Excel(Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetMaternityLeaveApplicationEmployeeList_Excel", filter);

        }

       public async Task<MaternityLeaveApplication> GetMaternityLeaveApplicationList(int leavePeriodCode, int leaveType,  string TabIndex, Guid? EmployeeGI)
        {

            return await _httpService.GetAsync<MaternityLeaveApplication>($"{_baseURL}/GetMaternityLeaveApplicationList?LeavePeriodCode={leavePeriodCode}&LeaveType={leaveType}&TabIndex={TabIndex}&EmployeeGI={EmployeeGI}");


        }

        public async Task<MaternityLeaveApplication> GetTrackerDetails(Guid LeaveApplicationGI)
        {
            return await _httpService.GetAsync<MaternityLeaveApplication>($"{_baseURL}/GetTrackerDetails?LeaveApplicationGI={LeaveApplicationGI}");
        }
    }
}
