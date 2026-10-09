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
   
    public class MonthlyAttendanceProcessingService : IMonthlyAttendanceProcessingService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public MonthlyAttendanceProcessingService(IServiceBase httpService)
        {
            _baseURL = "MonthlyAttendanceProcessing";
            _httpService = httpService;
        }

        async Task<MonthlyAttendanceProcessing> IMonthlyAttendanceProcessingService.Individual_MonthlyAttendanceProcessingList( int Month, int EmployeeCode, int Tabindex)
        {
            return await _httpService.GetAsync<MonthlyAttendanceProcessing>($"{_baseURL}/Individual_MonthlyAttendanceProcessingList?Month={Month}&EmployeeCode={EmployeeCode}&Tabindex={Tabindex}");
        }

        async Task<ResponseEntity> IMonthlyAttendanceProcessingService.Individual_ProcessingSave(MonthlyAttendanceProcessing model)
        {
            return await _httpService.PostAsync<ResponseEntity,MonthlyAttendanceProcessing>($"{_baseURL}/Individual_ProcessingSave", model);
        }

        async Task<MonthlyAttendanceProcessing> IMonthlyAttendanceProcessingService.MonthlyAttendanceProcessingAbbreviation()
        {
            return await _httpService.GetAsync<MonthlyAttendanceProcessing>($"{_baseURL}/MonthlyAttendanceProcessingAbbreviation");
        }




        async Task<MonthlyAttendanceProcessing> IMonthlyAttendanceProcessingService.GetBlankStatDaysPopList(Guid EmployeeGI, int Month)
        {
            return await _httpService.GetAsync<MonthlyAttendanceProcessing>($"{_baseURL}/GetBlankStatDaysPopList?EmployeeGI={EmployeeGI}&Month={Month}");
        }



        async Task<MonthlyAttendanceProcessing> IMonthlyAttendanceProcessingService.MonthlyAttendanceProcessingList(int Month, int PayrollGroup, int Tabindex, Filter filterdata)
        {
            return await _httpService.PostAsync<MonthlyAttendanceProcessing, Filter>($"{_baseURL}/MonthlyAttendanceProcessingList?Month={Month}&PayrollGroup={PayrollGroup}&Tabindex={Tabindex}", filterdata);
        }
        async Task<MonthlyAttendanceProcessing> IMonthlyAttendanceProcessingService.MonthlyAttendanceProcessingListPopUp(int Month, int PayrollGroup, int Tabindex, string Description, Filter filterdata)
        {
            return await _httpService.PostAsync<MonthlyAttendanceProcessing, Filter>($"{_baseURL}/MonthlyAttendanceProcessingListPopUp?Month={Month}&PayrollGroup={PayrollGroup}&Tabindex={Tabindex}&Description={Description}", filterdata);
        }
        async Task<ResponseEntity> IMonthlyAttendanceProcessingService.ProcessingSave(int Month, int PayrollGroup, int Tabindex, Filter filterdata)
        {
            return await _httpService.PostAsync<ResponseEntity, Filter>($"{_baseURL}/ProcessingSave?Month={Month}&PayrollGroup={PayrollGroup}&Tabindex={Tabindex}", filterdata);

        }
        async Task<ResponseEntity> IMonthlyAttendanceProcessingService.ProcessingPeriodLock(int Month, int PayrollGroup, int Tabindex, Filter filterdata)
        {
            return await _httpService.PostAsync<ResponseEntity, Filter>($"{_baseURL}/ProcessingPeriodLock?Month={Month}&PayrollGroup={PayrollGroup}&Tabindex={Tabindex}", filterdata);

        }

        public async Task<DataSet> MonthlyAttendanceProcessingList_Excel(int Month, int PayrollGroup, int Tabindex, Filter filterdata)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/MonthlyAttendanceProcessingList_Excel?Month={Month}&PayrollGroup={PayrollGroup}&Tabindex={Tabindex}", filterdata);
        }
        public async Task<MonthlyAttendanceProcessing> MonthlyAttendanceProcessingApprovalPendingPopUp(int Month, int PayrollGroup, int Tabindex, Filter filterdata)
        {
            return await _httpService.PostAsync<MonthlyAttendanceProcessing, Filter>($"{_baseURL}/MonthlyAttendanceProcessingApprovalPendingPopUp?Month={Month}&PayrollGroup={PayrollGroup}&Tabindex={Tabindex}", filterdata);
        }
        public async Task<DataSet> MonthlyAttendanceProcessingApprovalPendingPopUp_Excel(int Month, int PayrollGroup, int Tabindex, Filter filterdata)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/MonthlyAttendanceProcessingApprovalPendingPopUp_Excel?Month={Month}&PayrollGroup={PayrollGroup}&Tabindex={Tabindex}", filterdata);
        }
    }
}
