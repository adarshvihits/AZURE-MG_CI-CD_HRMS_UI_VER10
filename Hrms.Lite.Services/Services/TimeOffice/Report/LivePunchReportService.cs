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
  
    public  class LivePunchReportService:ILivePunchReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public LivePunchReportService(IServiceBase httpService)
        {
            _baseURL = "LivePunchReport";
            _httpService = httpService;
        }

        public async  Task<LivePunchReport> GetList(string Date, Filter filter)
        {
            return await _httpService.PostAsync<LivePunchReport, Filter>($"{_baseURL}/LivePunchReportList?Date={Date}",filter);
        }
        public async Task<DataSet> LivePunchReport_Excel(Filter filter, string Date)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/LivePunchReport_Excel?Date={Date}", filter);

        }
    }
}
