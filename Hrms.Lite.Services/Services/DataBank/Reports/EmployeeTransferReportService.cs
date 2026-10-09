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
    public class EmployeeTransferReportService : IEmployeeTransferReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public EmployeeTransferReportService(IServiceBase httpService)
        {
            _baseURL = "EmployeeTransferReport";
            _httpService = httpService;
        }
        public async Task<EmployeeTransferReport> GetEmployeeTransferReport(EmployeeTransferReport model)
        {
            return await _httpService.PostAsync<EmployeeTransferReport, EmployeeTransferReport>($"{_baseURL}/GetEmployeeTransferReport", model);
        }

      public async Task<DataSet> GetEmployeeTransferReport_Excel(EmployeeTransferReport model)
        {
            return await _httpService.PostAsync<DataSet, EmployeeTransferReport>($"{_baseURL}/GetEmployeeTransferReport_Excel", model);
        }
    }
}
