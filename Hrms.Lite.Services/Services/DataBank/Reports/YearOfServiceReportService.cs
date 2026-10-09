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
    public class YearOfServiceReportService : IYearOfServiceReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public YearOfServiceReportService(IServiceBase httpService)
        {
            _baseURL = "YearOfServiceReport";
            _httpService = httpService;
        }

        public async Task<YearOfServiceReport> GetYearOfServiceReport( int Type, string listCategory, Filter filterdata)
        {
            return await _httpService.PostAsync<YearOfServiceReport, Filter>($"{_baseURL}/GetYearOfServiceReport?Type={Type}&listCategory={listCategory}", filterdata);
        }

     

        public async Task<DataSet> YearOfServiceReport_Excel(int Type, string listCategory, Filter filterdata)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetYearOfServiceReport_Excel?Type={Type}&listCategory={listCategory}", filterdata);
        }

    }
}
