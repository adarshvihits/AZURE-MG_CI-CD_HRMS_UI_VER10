using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll.Reports;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.PayRoll.Reports
{
    public class PTReportService:IPTReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public PTReportService(IServiceBase httpService)
        {
            _baseURL = "PTReport";
            _httpService = httpService;
        }
        public async Task<PTReport> GetPTReport(int SalaryPeriod, int PtGroup, FilterMultiSelect filter)
        {
            return await _httpService.PostAsync<PTReport, FilterMultiSelect>($"{_baseURL}/GetPTReport?SalaryPeriod={SalaryPeriod}&PtGroup={PtGroup}", filter);
        }
        public async Task<DataSet> GetPTReport_Excel(int SalaryPeriod, int PtGroup, FilterMultiSelect filter)
        {
            return await _httpService.PostAsync<DataSet, FilterMultiSelect>($"{_baseURL}/GetPTReport_Excel?SalaryPeriod={SalaryPeriod}&PtGroup={PtGroup}", filter);
        }

    }
}
