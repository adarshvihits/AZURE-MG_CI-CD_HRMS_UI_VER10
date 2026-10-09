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
    public class CanteenDateWiseReportService: ICanteenDateWiseReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public CanteenDateWiseReportService(IServiceBase httpService)
        {
            _baseURL = "CanteenDateWiseReport";
            _httpService = httpService;
        }
        public async Task<CanteenDateWiseReport> GetEmployeeList(CanteenDateWiseReport model)
        {
            return await _httpService.PostAsync<CanteenDateWiseReport, CanteenDateWiseReport>($"{_baseURL}/GetEmployeeList", model);
        }
        public async Task<DataSet> GetEmployeeList_Excel(CanteenDateWiseReport model)
        {
            return await _httpService.PostAsync<DataSet, CanteenDateWiseReport>($"{_baseURL}/GetEmployeeList_Excel",model);
        }
    }
}
