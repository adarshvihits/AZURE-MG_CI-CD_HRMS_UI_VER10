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
    public class ExceptionEmployeesService: IExceptionEmployeesService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public ExceptionEmployeesService(IServiceBase httpService)
        {
            _baseURL = "ExceptionEmployees";
            _httpService = httpService;
        }
        public async Task<ExceptionEmployees> GetEmployeeList(int MonthCode, int BasedOn, Filter Filter)
        {
            return await _httpService.PostAsync<ExceptionEmployees, Filter>($"{_baseURL}/List?MonthCode={MonthCode}&BasedOn={BasedOn}", Filter);
        }

        public async Task<ResponseEntity> AddEmployee(Guid EmployeeGI, string Type, int MonthCode,string remarks)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL}/AddEmployee?EmployeeGI={EmployeeGI}&Type={Type}&MonthCode={MonthCode}&remarks={remarks}");
        }
        public async Task<ResponseEntity> BatchUpdate(int MonthCode, int BasedOn,string TypeValue, Filter Filter)
        {
            return await _httpService.PostAsync<ResponseEntity, Filter>($"{_baseURL}/BatchUpdate?MonthCode={MonthCode}&BasedOn={BasedOn}&TypeValue={TypeValue}", Filter);
        }
        public async Task<DataSet> EmployeeList_Excel(int MonthCode, int BasedOn, Filter Filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/EmployeeList_Excel?MonthCode={MonthCode}&BasedOn={BasedOn}", Filter);
        }
        public async Task<ResponseEntity<List<ExceptionEmployees>>> ImportFromExcel(ExceptionEmployees model)
        {
            return await _httpService.PostAsync<ResponseEntity<List<ExceptionEmployees>>, ExceptionEmployees>($"{_baseURL}/ImportExcel", model);

        }


        public async Task<ExceptionEmployees> TransactionHistoryList(int MonthCode, Filter filter)
        {
            return await _httpService.PostAsync<ExceptionEmployees, Filter>($"{_baseURL}/TransactionHistoryList?MonthCode={MonthCode}", filter);

        }


        public async Task<DataSet> HistoryEmployeeList_Excel(int MonthCode, Filter Filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/HistoryEmployeeList_Excel?MonthCode={MonthCode}", Filter);
        }
    }
}
