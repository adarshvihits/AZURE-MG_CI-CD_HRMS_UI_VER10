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
    public class SalaryStructureReportService: ISalaryStructureReportService
    {

        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public SalaryStructureReportService(IServiceBase httpService)
        {
            _baseURL = "SalaryStructureReport";
            _httpService = httpService;
        }
        public async Task<SalaryStructureReport> GetList(int TabIndex, Filter filter)
        {
            return await _httpService.PostAsync<SalaryStructureReport,Filter>($"{_baseURL}/GetSalaryStructureReportList?TabIndex={TabIndex}",filter);
        }

        public async Task<SalaryStructureReport> GetSalaryStructureList(int TabIndex, Filter filter)
        {
            return await _httpService.PostAsync<SalaryStructureReport, Filter>($"{_baseURL}/GetSalaryStructureReport?TabIndex={TabIndex}", filter);
        }

        public async Task<DataSet> GetSalaryStructureReportHeaders(int TabIndex, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetSalaryStructureReportHeaders?TabIndex={TabIndex}", filter);
        }
        public async Task<DataSet> GetSalaryStructureReportData(int TabIndex, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetSalaryStructureReportData?TabIndex={TabIndex}", filter);
        }
    }
}
