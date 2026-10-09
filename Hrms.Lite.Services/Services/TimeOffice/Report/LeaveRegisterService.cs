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
    public class LeaveRegisterService : ILeaveRegisterService
    {

        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public LeaveRegisterService(IServiceBase httpService)
        {
            _baseURL = "LeaveRegister";
            _httpService = httpService;
        }
        public async Task<LeaveRegister> GetGridSettings()
        {
            return await _httpService.GetAsync<LeaveRegister>($"{_baseURL}/GetGridSettings");
        }

     public async Task<LeaveRegister> GetLeaveRegisterReports(int LeavePeriod,int TabIndex, LeaveRegister model)
        {
            return await _httpService.PostAsync<LeaveRegister, LeaveRegister>($"{_baseURL}/LeaveRegisterReport?LeavePeriod={LeavePeriod}&TabIndex={TabIndex}", model);
        }



        public async Task<DataSet> GetLeaveRegisterReports_Excel(int LeavePeriod,int TabIndex, LeaveRegister model)
        {
            return await _httpService.PostAsync<DataSet, LeaveRegister>($"{_baseURL}/GetLeaveRegisterReports_Excel?LeavePeriod={LeavePeriod}&TabIndex={TabIndex}", model);
        }
        //AnnualLeaveRegister
        public async Task<AnnualLeaveRegister> AnnualLeaveRegister(int FinancialYearCode, int EmployeeCode, Filter Filter)
        {
            return await _httpService.PostAsync<AnnualLeaveRegister, Filter>($"{_baseURL}/AnnualLeaveRegister?FinancialYearCode={FinancialYearCode}&EmployeeCode={EmployeeCode}", Filter);
        }
        public async Task<DataSet> AnnualLeaveRegisterExcel(int FinancialYearCode, int EmployeeCode, Filter Filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/AnnualLeaveRegisterExcel?FinancialYearCode={FinancialYearCode}&EmployeeCode={EmployeeCode}", Filter);
        }
    }
}
