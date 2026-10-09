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
    public class ReplacementOrNewExpReportService: IReplacementOrNewExpReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public ReplacementOrNewExpReportService(IServiceBase httpService)
        {
            _baseURL = "ReplacementOrNewExpReport";
            _httpService = httpService;
        }
        public async Task<ReplacementOrNewExpReport> GetList(Filter filter)
        {
            return await _httpService.PostAsync<ReplacementOrNewExpReport, Filter>($"{_baseURL}/GetList", filter);
        }

        public async Task<DataSet> GetList_Excel( Filter Filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetList_Excel", Filter);
        }
    }
}
