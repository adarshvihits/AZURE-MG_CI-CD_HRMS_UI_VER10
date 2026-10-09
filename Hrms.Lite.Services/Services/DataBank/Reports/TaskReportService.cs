using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank.Reports
{
    public class TaskReportService: ITaskReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public TaskReportService(IServiceBase httpService)
        {
            _baseURL = "EmployeeTaskReport";
            _httpService = httpService;
        }
        public async Task<EmployeeTaskReport> TaskReportList(string Status, Filter filter)
        {
            return await _httpService.PostAsync<EmployeeTaskReport, Filter>($"{_baseURL}/TaskReportList?status={Status}", filter);
        }
        public async Task<DataSet> GetTaskReport_Excel(string Status, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/TaskReportList_Excel?status={Status}", filter);
        }
        public async Task<EmployeeTaskReport> GetTaskReportList(string Status, Filter filter)
        {
            return await _httpService.PostAsync<EmployeeTaskReport, Filter>($"{_baseURL}/GetTaskReportList?status={Status}", filter);
        }
        public async Task<DataSet> TaskReport_Excel(string Status, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/TaskReport_Excel?status={Status}", filter);
        }

        public async Task<EmployeeTaskReport> GetActionDetails(int TaskCode)
        {
            return await _httpService.GetAsync<EmployeeTaskReport>($"{_baseURL}/GetActionDetails?TaskCode={TaskCode}");
        }
    }
}
