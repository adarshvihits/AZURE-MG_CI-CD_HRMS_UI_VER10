using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll.Reports;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll.Reports;
using Hrms.Lite.Shared.TimeOffice;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.PayRoll.Reports
{
  public  class ArrearProcessingReportService:IArrearProcessingReportService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public ArrearProcessingReportService(IServiceBase httpService)
        {
            _baseURL = "ArrearProcessingReport";
            _httpService = httpService;
        }


        public async Task<ArrearProcessingReport> GetArrearProcessingReport(int MonthCode, Filter filter)
        {
            return await _httpService.PostAsync<ArrearProcessingReport, Filter>($"{_baseURL}/GetArrearProcessingReport?MonthCode={MonthCode}", filter);
        }
     
        public async Task<DataSet> GetArrearProcessingReport_Excel(int MonthCode, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetArrearProcessingReport_Excel?MonthCode={MonthCode}", filter);

        }

    }
}
