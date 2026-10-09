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
    public class AttendanceWagesConfirmationService : IAttendanceWagesConfirmationService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public AttendanceWagesConfirmationService(IServiceBase httpService)
        {
            _baseURL = "AttendanceWagesConfirmation";
            _httpService = httpService;
        }

        public Task<ResponseEntity> AttendanceWagesConfirmationSave(AttendanceWagesConfirmation model)
        {
            return _httpService.PostAsync<ResponseEntity, AttendanceWagesConfirmation>($"{_baseURL}/AttendanceWagesConfirmationSave", model);
        }

        public async Task<AttendanceWagesConfirmation> EmployeeWiseDetails(string TabIndex, int month, int Value, Guid EmployeeGI)
        {
            return await _httpService.GetAsync<AttendanceWagesConfirmation>($"{_baseURL}/GetEmployeeAttnDetails?Month={month}&TabIndex={TabIndex}&ConfirmationStatus={Value}&EmployeeGI={EmployeeGI}");
        }

        public  Task<ResponseEntity> EmployeeWiseSave(AttendanceWagesConfirmation model)
        {
            return _httpService.PostAsync<ResponseEntity, AttendanceWagesConfirmation>($"{_baseURL}/EmployeeWiseAttendanceWagesConfirmationSave", model);
        }
        public  Task<ResponseEntity> EmployeeWiseAttendanceWagesConfirmedValueUpdate(AttendanceWagesConfirmation model)
        {
            return _httpService.PostAsync<ResponseEntity, AttendanceWagesConfirmation>($"{_baseURL}/EmployeeWiseAttendanceWagesConfirmedValueUpdate", model);
        }

        public async Task<AttendanceWagesConfirmation> GetConfirmationList(string TabIndex, int PGroup, int month, int ConfirmationStatus, Filter filter)
        {
            return await _httpService.PostAsync<AttendanceWagesConfirmation, Filter>($"{_baseURL}/ConfirmationList?Month={month}&PGroup={PGroup}&TabIndex={TabIndex}&ConfirmationStatus={ConfirmationStatus}", filter);
        }

        public async Task<DataSet> GetEmployeeAttnDetails_Excel(int month, string TabIndex, int ConfirmationStatus, Guid EmployeeGI)
        {
            return await _httpService.GetAsync<DataSet>($"{_baseURL}/GetEmployeeAttnDetails_Excel?Month={month}&TabIndex={TabIndex}&ConfirmationStatus={ConfirmationStatus}&EmployeeGI={EmployeeGI}");
        }
      
        public async Task<AttendanceWagesConfirmation> GetEmployeeWiseData(int month,string TabIndex, int ConfirmationStatus, Guid EmployeeGI)
        {
            return await _httpService.GetAsync<AttendanceWagesConfirmation>($"{_baseURL}/GetEmployeeWiseData?Month={month}&TabIndex={TabIndex}&ConfirmationStatus={ConfirmationStatus}&EmployeeGI={EmployeeGI}");
        }

      public  Task<ResponseEntity> BatchUpdate(AttendanceWagesConfirmation model)
        {
            return _httpService.PostAsync<ResponseEntity, AttendanceWagesConfirmation>($"{_baseURL}/BatchUpdate", model);

        }

        public async Task<AttendanceWagesConfirmation> ConfirmationList_GEO(string Tab, Filter filter, int Month)
        {
            return await _httpService.PostAsync<AttendanceWagesConfirmation, Filter>($"{_baseURL}/ConfirmationList_GEO?Tab={Tab}&Month={Month}", filter);

        }
        public async Task<ResponseEntity> EditUpdate(AttendanceWagesConfirmation Model)
        {
            return await _httpService.PostAsync<ResponseEntity, AttendanceWagesConfirmation>($"{_baseURL}/EditUpdate", Model);

        }
        public async Task<ResponseEntity> LessHourConfirmationSave(Filter filter, int Month)
        {
            return await _httpService.PostAsync<ResponseEntity, Filter>($"{_baseURL}/LessHourConfirmationSave?Month={Month}", filter);

        }
        public async Task<DataSet> GetConfirmationList_Excel(string Tab, Filter filter, int Month)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetConfirmationList_Excel?Tab={Tab}&Month={Month}", filter);

        }

        public async Task<AttendanceWagesConfirmation> LHCDetails(int Month, int EmployeeCode)
        {
            return await _httpService.GetAsync<AttendanceWagesConfirmation>($"{_baseURL}/LHCDetails?Month={Month}&EmployeeCode={EmployeeCode}");
        }
    }
}                                                                                                                                                     
