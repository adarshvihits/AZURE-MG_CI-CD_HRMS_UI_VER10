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
    public class ESIReportService : IESIReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public ESIReportService(IServiceBase httpService)
        {
            _baseURL = "EsiEcrReport";
            _httpService = httpService;
        }

        public async Task<EsiEcrReport> GetReportList(int MonthCode, int EsiGroup, Filter filter)
        {
            return await _httpService.PostAsync<EsiEcrReport, Filter>($"{_baseURL}/GetEsiEcrReport?Month={MonthCode}&ESIGroup={EsiGroup}", filter);
        }

       public async Task<DataSet> GetEsiEcrReport_Excel(int Month, int ESIGroup, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetEsiEcrReport_Excel?Month={Month}&ESIGroup={ESIGroup}", filter);

        }
    }
}
