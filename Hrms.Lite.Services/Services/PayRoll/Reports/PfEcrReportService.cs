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
    public class PfEcrReportService: IPfEcrReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public PfEcrReportService(IServiceBase httpService)
        {
            _baseURL = "PfEcrReport";
            _httpService = httpService;
        }
        public async Task<PfEcrReport> GetList(int Month, int PFGroup,int format, Filter filter)
        {
            return await _httpService.PostAsync<PfEcrReport, Filter>($"{_baseURL}/GetPfEcrReport?Month={Month}&PFGroup={PFGroup}&Type={format}", filter);
        }

        public async Task<DataSet> GetPfEcrReport_Excel(int Month, int PFGroup, Filter filter, int Type)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetPfEcrReport_Excel?Month={Month}&PFGroup={PFGroup}&Type={Type}", filter);

        }
    }
}
