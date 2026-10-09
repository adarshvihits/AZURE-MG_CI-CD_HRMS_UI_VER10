using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TimeOffice;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.TimeOffice
{
    public class LeaveCancellationService : ILeaveCancellationService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public LeaveCancellationService(IServiceBase httpService)
        {
            _baseURL = "LeaveCancellation";
            _httpService = httpService;
        }
        public async Task<LeaveCancellation> GetEmployeeLeaveCancellationList(int PayrollCode,int MonthCode, int LeaveCode, Filter filter)
        {
            return await _httpService.PostAsync<LeaveCancellation, Filter>($"{_baseURL}/GetEmployeeLeaveCancellationList?MonthCode={MonthCode}&PayrollGroup={PayrollCode}&LeaveCode={LeaveCode}", filter);
        }
        public async Task<ResponseEntity> LeaveCancellationSave(LeaveCancellation model)
        {
            return await _httpService.PostAsync<ResponseEntity, LeaveCancellation>($"{_baseURL}/LeaveCancellationSave", model);
        }
        public async Task<DataSet> GetEmployeeLeaveCancellationList_Excel(int PayrollCode,int MonthCode,int LeaveCode, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetEmployeeLeaveCancellationList_Excel?MonthCode={MonthCode}&PayrollGroup={PayrollCode}&LeaveCode={LeaveCode}", filter);

        }
        public async Task<LeaveCancellation> GetLeaveCancellationLog(int PayrollCode,int MonthCode, int LeaveCode, Filter filter)
        {
            return await _httpService.PostAsync<LeaveCancellation, Filter>($"{_baseURL}/GetLeaveCancellationLog?MonthCode={MonthCode}&PayrollGroup={PayrollCode}&LeaveCode={LeaveCode}", filter);
        }
        public async Task<DataSet> GetLeaveCancellationLog_Excel(int PayrollCode, int MonthCode, int LeaveCode, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetLeaveCancellationLog_Excel?MonthCode={MonthCode}&PayrollGroup={PayrollCode}&LeaveCode={LeaveCode}", filter);

        }
    }

}
