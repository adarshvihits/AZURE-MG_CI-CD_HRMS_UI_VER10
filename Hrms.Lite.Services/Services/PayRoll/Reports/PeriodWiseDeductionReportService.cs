using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.PayRoll.Reports
{
  public  class PeriodWiseDeductionReportService: IPeriodWiseDeductionReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public PeriodWiseDeductionReportService(IServiceBase httpService)
        {
            _baseURL = "PeriodicDeductionReport";
            _httpService = httpService;
        }
        public async Task<DataSet> GetPeriodicDeductionReport(Filter filter, int Deduction, int MonthFrom, int MonthTo)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetPeriodicDeductionReport?Deduction={Deduction}&MonthFrom={MonthFrom}&MonthTo={MonthTo}", filter);

        }
        public async Task<DataSet> GetPeriodicDeductionReport_Excel(Filter filter, int Deduction, int MonthFrom, int MonthTo)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetPeriodicDeductionReport_Excel?Deduction={Deduction}&MonthFrom={MonthFrom}&MonthTo={MonthTo}", filter);

        }
    }
}
