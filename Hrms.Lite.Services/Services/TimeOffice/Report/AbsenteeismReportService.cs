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
  
    public class AbsenteeismReportService : IAbsenteeismReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public AbsenteeismReportService(IServiceBase httpService)
        {
            _baseURL = "AbsenteeismReport";
            _httpService = httpService;
        }
        public async Task<AbsenteeismReport> GetAbsenteeismReportList(string fromDate, string Todate, int Type,Filter filter)
        {
            return await _httpService.PostAsync<AbsenteeismReport, Filter>($"{_baseURL}/GetReport?FromDate={fromDate}&ToDate={Todate}&Type={Type}", filter);
        }

      public async Task<DataSet> AbsenteeismReport_Excel(string FromDate, string ToDate, int Type, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/AbsenteeismReport_Excel?FromDate={FromDate}&ToDate={ToDate}&Type={Type}", filter);

        }
        public async Task<AbsenteeismReport> DateWiseStatusPopUp(string date,Filter filter)
        {
            return await _httpService.PostAsync<AbsenteeismReport,Filter>($"{_baseURL}/GetDateWiseAttnStatusDetails?Date=" + date,filter);

        }
        public async Task<AbsenteeismReport> EmpWiseStatusPopUp(Guid EmployeeGI, string fromDate, string Todate,Filter filter)
        {
            return await _httpService.PostAsync<AbsenteeismReport,Filter>($"{_baseURL}/GetEmployeeWiseAttnStatusDetails?EmployeeGI={EmployeeGI}&FromDate={fromDate}&ToDate={Todate}", filter);

        }
        //public async Task<AbsenteeismReport> GetAbsenteeismReasonPopUp(int Value, int column, Guid EmpGI)
        //{
        //    return await _httpService.GetAsync<AbsenteeismReport>($"{_baseURL}/GetDateWiseReasonDetails?Value={Value}&Type={column}&EmpGI={EmpGI}");

        //}
        public async Task<AbsenteeismReport> DateWiseReasonPopUp(string date, int column, Filter filter)
        {
            return await _httpService.PostAsync<AbsenteeismReport, Filter>($"{_baseURL}/GetDateWiseReasonDetails?Date={date}&Type={column}", filter);

        }
        public async Task<AbsenteeismReport> EmpWiseReasonPopUp(Guid EmployeeGI, string fromDate, string Todate, int column, Filter filter)
        {
            return await _httpService.PostAsync<AbsenteeismReport,Filter>($"{_baseURL}/GetEmployeeWiseReasonDetails?EmployeeGI={EmployeeGI}&FromDate={fromDate}&ToDate={Todate}&Type={column}",filter);

        }

       
    }
}
