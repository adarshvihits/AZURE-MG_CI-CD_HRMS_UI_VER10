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
    public class ExitInterviewReportService : IExitInterviewReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public ExitInterviewReportService(IServiceBase httpService)
        {
            _baseURL = "ExitInterviewReport";
            _httpService = httpService;
        }

        public async Task<ExitInterviewReport> GetExitInterviewReportList(Filter model)
        {
            return await _httpService.PostAsync<ExitInterviewReport, Filter>($"{_baseURL}/GetExitInterviewReportList", model);
        }
        public async Task<DataSet> GetExitInterviewReport_Excel(Filter model)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetExitInterviewReport_Excel", model);
        }
        public async Task<ExitInterviewReport> GetExitInterviewReportPopUpDetails(int EmployeeCode)
        {
            return await _httpService.GetAsync<ExitInterviewReport>($"{_baseURL}/GetExitInterviewReportPopUpDetails?EmployeeCode={EmployeeCode}");

        }
        public async Task<DataSet> GetExitInterviewReportPopUp_Excel(int EmployeeCode)
        {
            return await _httpService.GetAsync<DataSet>($"{_baseURL}/GetExitInterviewReportPopUp_Excel?EmployeeCode={EmployeeCode}");
        }
    }
}
