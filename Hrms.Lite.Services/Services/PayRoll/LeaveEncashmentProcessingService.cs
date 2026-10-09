using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.PayRoll
{
    public class LeaveEncashmentProcessingService : ILeaveEncashmentProcessingService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public LeaveEncashmentProcessingService(IServiceBase httpService)
        {
            _baseURL = "LeaveEncashmentProcessing";
            _httpService = httpService;
        }

        public async Task<DataSet> Encashment_excel(int Month, int Allowance, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/EmployeeLeaveEncashmentList_Excel?Month={Month}", filter);
        }

        public async  Task<DataSet> Encashment_excel_Report(int Month, int Allowance, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetLeaveEncashmentProcessedReport_Excel?MonthCode={Month}&AllowanceCode={Allowance}", filter);
        }

        public async Task<LeaveEncashmentProcessing> GetList(int Month, Filter filter)
        {
            return await _httpService.PostAsync<LeaveEncashmentProcessing, Filter>($"{_baseURL}/EmployeeLeaveEncashmentList?Month={Month}", filter);
        }

        public async Task<LeaveEncashmentProcessing> GetListReport(int Month, int Allowance, Filter filter)
        {
            return await _httpService.PostAsync<LeaveEncashmentProcessing, Filter>($"{_baseURL}/GetLeaveEncashmentProcessedReport?MonthCode={Month}&AllowanceCode={Allowance}", filter);
        }

        public async  Task<ResponseEntity> Save(LeaveEncashmentProcessing model)
        {
            return await _httpService.PostAsync<ResponseEntity, LeaveEncashmentProcessing>($"{_baseURL}/LeaveEncashmentProcessingSave", model);
        }
    }
}
