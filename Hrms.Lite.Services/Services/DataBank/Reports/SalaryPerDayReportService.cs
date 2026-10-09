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
   public class SalaryPerDayReportService:ISalaryPerDayReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public SalaryPerDayReportService(IServiceBase httpService)
        {
            _baseURL = "SalaryPerDayReport";
            _httpService = httpService;
        }

        public async Task<SalaryPerDayReport> GetList(SalaryPerDayReport model)
        {
            return await _httpService.PostAsync<SalaryPerDayReport, SalaryPerDayReport>($"{_baseURL}/GetSalaryPerDayList", model);
        }
        public async Task<DataSet> GetList_Excel(SalaryPerDayReport model)
        {
            return await _httpService.PostAsync<DataSet, SalaryPerDayReport>($"{_baseURL}/GetSalaryPerDayList_Excel", model);
        }
    }
}
