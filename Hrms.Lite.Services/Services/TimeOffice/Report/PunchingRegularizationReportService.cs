using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TimeOffice.Report;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.TimeOffice.Report
{
   
    public class PunchingRegularizationReportService: IPunchingRegularizationReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public PunchingRegularizationReportService(IServiceBase httpService)
        {
            _baseURL = "PunchingRegularizationReport";
            _httpService = httpService;
        }

        public async Task<PunchingRegularizationReport> GetRegularizationList(string TabIndex, int Month, int Reason, int LeavePeriod,Filter filter)
        {
            return await _httpService.PostAsync<PunchingRegularizationReport, Filter>($"{_baseURL}/GetRegularizationList?TabIndex={TabIndex}&Month={Month}&Reason={Reason}&LeavePeriod={LeavePeriod}", filter);
        }

        public async Task<DataSet> GetRegularizationList_Excel(string TabIndex, int Month, int Reason, int LeavePeriod, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetRegularizationList_Excel?TabIndex={TabIndex}&Month={Month}&Reason={Reason}&LeavePeriod={LeavePeriod}", filter);
        }
    }
}
