using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TimeOffice
{
    public class DailyAttendanceEntryService : IDailyAttendanceEntryService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public DailyAttendanceEntryService(IServiceBase httpService)
        {
            _baseURL = "DailyAttendanceEntry";
            _httpService = httpService;
        }

        public async Task<DailyAttendanceEntry> DailyAttendanceEntryList(int Month, int PayrollGroup, int Tabindex, Filter filter)
        {
            return await _httpService.PostAsync<DailyAttendanceEntry, Filter>($"{_baseURL}/DailyAttendanceEntryList?Month={Month}&PayrollGroup={PayrollGroup}&Tabindex={Tabindex}", filter);
        }


        public async Task<DailyAttendanceEntryPaging> DailyAttendanceEntryPagingList(int Month, int PayrollGroup, int Tabindex, Filter filter)
        {
            return await _httpService.PostAsync<DailyAttendanceEntryPaging, Filter>($"{_baseURL}/DailyAttendanceEntryPagingList?Month={Month}&PayrollGroup={PayrollGroup}&Tabindex={Tabindex}", filter);
        }
        public async Task<DataSet> DailyAttendanceEntryList_Excel(int Month, int PayrollGroup, int Tabindex, Filter filterdata)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/DailyAttendanceEntryList_Excel?Month={Month}&PayrollGroup={PayrollGroup}&Tabindex={Tabindex}", filterdata);
        }
        public async Task<DataSet> EmployeeWiseDailyAttendanceEntryList_Excel(int Month, Filter filter, int PgGroup)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/EmployeeWiseDailyAttendanceEntryList_Excel?Month={Month}&PgGroup={PgGroup}", filter);
        }
        public async Task<DailyAttendanceEntry> DailyAttendanceEntryEditList(string Date, int PayrollGroup, int AttnStatus, Filter filter)
        {
            return await _httpService.PostAsync<DailyAttendanceEntry, Filter>($"{_baseURL}/DailyAttendanceEntryEditList?Date={Date}&PayrollGroup={PayrollGroup}&AttnStatus={AttnStatus}", filter);

        }


        public async Task<ResponseEntity> DailyattendanceEntry(DailyAttendanceEntry model)
        {
            return await _httpService.PostAsync<ResponseEntity, DailyAttendanceEntry>($"{_baseURL}/DailyAttendanceEntry", model);
        }

        public async Task<ResponseEntity> DailyattendanceEntryBatchUpdate(DailyAttendanceEntry model)
        {
            return await _httpService.PostAsync<ResponseEntity, DailyAttendanceEntry>($"{_baseURL}/DailyattendanceEntryBatchUpdate", model);
        }
        public async Task<ResponseEntity> EmployeeWiseAttendanceEntryBatchUpdate(DailyAttendanceEntry model)
        {
            return await _httpService.PostAsync<ResponseEntity, DailyAttendanceEntry>($"{_baseURL}/EmployeeWiseAttendanceEntryBatchUpdate", model);
        }
        public async Task<DailyAttendanceEntry> DailyAttendanceEntryEditDetails(string Date, Guid EmployeeGI)
        {
            return await _httpService.GetAsync<DailyAttendanceEntry>($"{_baseURL}/DailyAttendanceEntryEditDetails?Date={Date}&EmployeeGI={EmployeeGI}");
        }

        public async Task<DailyAttendanceEntry> EmployeeLeaveDetails(int Month, Guid EmployeeGI)
        {
            return await _httpService.GetAsync<DailyAttendanceEntry>($"{_baseURL}/EmployeeLeaveDetails?Month={Month}&EmployeeGI={EmployeeGI}");
        }


        public async Task<DailyAttendanceEntry> EmployeeDailyAttendanceEntryEditDetails(int Month, int ABSTATUS, Guid EmployeeGI)
        {

            return await _httpService.GetAsync<DailyAttendanceEntry>($"{_baseURL}/EmployeeDailyAttendanceEntryEditDetails?Month={Month}&ABSTATUS={ABSTATUS}&EmployeeGI={EmployeeGI}");

        }

        public async Task<DailyAttendanceEntry> EmployeeWiseDailyAttendanceEntryList(int Month, Filter filter, int PgGroup)
        {
            return await _httpService.PostAsync<DailyAttendanceEntry, Filter>($"{_baseURL}/EmployeeWiseDailyAttendanceEntryList?Month={Month}&PgGroup={PgGroup}", filter);

        }
        async Task<ResponseEntity> IDailyAttendanceEntryService.GetDailyAttendanceEntryDateValidation(string SelectedDate, int? PayrollGroup, int? EmployeeCode)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL}/GetDailyAttendanceEntryDateValidation?SelectedDate={SelectedDate}&PayrollGroup={PayrollGroup}&EmployeeCode={EmployeeCode}");

        }

        //*******************************************DailyAttendanceProcessing****************************************************************************
        public async Task<DailyAttendanceEntry> DailyAttendanceProcessing(string Date, Filter filterdata)
        {
            return await _httpService.PostAsync<DailyAttendanceEntry, Filter>($"{_baseURL}/DailyAttendanceProcessing?Date={Date}", filterdata);
        }

        async Task<DailyAttendanceEntry> IDailyAttendanceEntryService.DailyAttendanceProcessing_Details(string Date, int StatusCode, Filter filterdata)
        {
            return await _httpService.PostAsync<DailyAttendanceEntry, Filter>($"{_baseURL}/DailyAttendanceProcessing_Details?Date={Date}&StatusCode={StatusCode}", filterdata);
        }

        public async Task<DataSet> DailyAttendanceProcessing_Details_Excel(string Date, int StatusCode, Filter filterdata)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/DailyAttendanceProcessing_Details_Excel?Date={Date}&StatusCode={StatusCode}", filterdata);
        }

        public async Task<ResponseEntity<DailyAttendanceEntry>> DailyAttendanceProcessingValidation(string SelectedDate)
        {
            return await _httpService.GetAsync<ResponseEntity<DailyAttendanceEntry>>($"{_baseURL}/DailyAttendanceProcessingValidation?SelectedDate={SelectedDate}");
        }

        //*********************Daily attendance processing-individual*********************


        public async Task<DailyAttendanceEntry> Individual_DailyAttendanceProcessing(string Date, int EmployeeCode)
        {
            return await _httpService.GetAsync<DailyAttendanceEntry>($"{_baseURL}/Individual_DailyAttendanceProcessing?Date={Date}&EmployeeCode={EmployeeCode}");



        }

    }
}
