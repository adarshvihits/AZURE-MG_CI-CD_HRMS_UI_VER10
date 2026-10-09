using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank.Reports
{
    public class RetirementDueReportService: IRetirementDueReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public RetirementDueReportService(IServiceBase httpService)
        {
            _baseURL = "RetirementDueReport";
            _httpService = httpService;
        }
        public async Task<JobTransition> GetRetirementDueReport(int Tabindex, string FromDate, string ToDate, Filter filter)
        {
            return await _httpService.PostAsync<JobTransition, Filter>($"{_baseURL}/GetRetirementDueReport?Tabindex={Tabindex}&FromDate={FromDate}&ToDate={ToDate}", filter);
        }
        public async Task<DataSet> GetRetirementDueReport_Excel(int Tabindex, string FromDate, string ToDate, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetRetirementDueReport_Excel?Tabindex={Tabindex}&FromDate={FromDate}&ToDate={ToDate}", filter);

        }
    }
}
