using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Shared.DataBank.Reports;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank.Reports
{
    public class EmployeeGroupSummaryReportService : IEmployeeGroupSummaryReportService
    {

        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public EmployeeGroupSummaryReportService(IServiceBase httpService)
        {
            _baseURL = "EmployeeGroupSummaryReport";
            _httpService = httpService;
        }
        public async Task<EmployeeGroupSummaryReport> GetEmployeeGroupSummaryReportList(EmployeeGroupSummaryReport model)
        {
            return await _httpService.PostAsync<EmployeeGroupSummaryReport, EmployeeGroupSummaryReport>($"{_baseURL}/GetEmployeeGroupSummaryReport", model);
        }


        public async Task<EmployeeGroupSummaryReport> GetPopUp(int GroupbyY)
        {
            return await _httpService.GetAsync<EmployeeGroupSummaryReport>($"{_baseURL}/GetEmployeeGroupSummarySelectList?GroupbyY=" + GroupbyY);

        }

    }
}
