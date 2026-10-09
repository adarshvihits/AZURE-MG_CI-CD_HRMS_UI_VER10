using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Shared.DataBank.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank.Reports
{
    public class EmployeeInformationReportService: IEmployeeInformationReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public EmployeeInformationReportService(IServiceBase httpService)
        {
            _baseURL = "EmployeeInformationReport";
            _httpService = httpService;
        }
        public Task<System.Data.DataSet> GetDetails(EmployeeInformationReport model)
        {
            return _httpService.PostAsync<System.Data.DataSet, EmployeeInformationReport>($"{_baseURL}/GetDetails", model);
        }

      public  Task<EmployeeInformationReport> GetPersonalDetailsDetailedReport(EmployeeInformationReport model)
        {
            return _httpService.PostAsync<EmployeeInformationReport, EmployeeInformationReport>($"{_baseURL}/GetPersonalDetailsDetailedReport", model);

        }

       public Task<DataSet> GetPersonalDetailsDetailedReport_Excel(EmployeeInformationReport model)
        {
            return _httpService.PostAsync<DataSet, EmployeeInformationReport>($"{_baseURL}/GetPersonalDetailsDetailedReport_Excel", model);
        }
    }
}
