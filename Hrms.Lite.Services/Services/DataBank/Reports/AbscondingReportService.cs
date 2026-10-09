using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank.Reports
{
  public class AbscondingReportService :IAbscondingReportService
    {

        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public AbscondingReportService(IServiceBase httpService)
        {
            _baseURL = "AbscondingReport";
            _httpService = httpService;
        }
        public async Task<AbscondingReport> GetAbscondingReport(int DayCode, Filter model)
        {
            return await _httpService.PostAsync<AbscondingReport, Filter>($"{_baseURL}/GetAbscondingReport?DayCode={DayCode}", model);
        }

        public async Task<DataSet> GetAbscondingReport_Excel( Filter filter, int DayCode)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetAbscondingReport_Excel?DayCode={DayCode}", filter);

        }
    }
}
